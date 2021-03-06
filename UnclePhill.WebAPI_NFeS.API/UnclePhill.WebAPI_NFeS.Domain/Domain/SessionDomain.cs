﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnclePhill.WebAPI_NFeS.Models;
using UnclePhill.WebAPI_NFeS.Models.Models;
using UnclePhill.WebAPI_NFeS.Utils.Utils;

namespace UnclePhill.WebAPI_NFeS.Domain
{
    public static class SessionDomain
    {
        private static StringBuilder SQL = new StringBuilder();
        private const string Timeout = "60"; 
        public static string SessionHash;

        public static string GenerateHash(string Value)
        {
            try
            {
                return Functions.Encript(Value + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CheckSession(string SessionHash)
        {
            try
            {
                UpdateSession();

                if (string.IsNullOrEmpty(SessionHash))
                {
                    throw new InternalProgramException("Sessão inválida!");
                }

                SQL = new StringBuilder();
                SQL.AppendLine(" Select ");
                SQL.AppendLine("    SessionId, ");
                SQL.AppendLine("    UserId, ");
                SQL.AppendLine("    SessionHash, ");
                SQL.AppendLine("    DateStart, ");
                SQL.AppendLine("    DateEnd, ");
                SQL.AppendLine("    Active, ");
                SQL.AppendLine("    DateInsert, ");
                SQL.AppendLine("    DateUpdate ");
                SQL.AppendLine(" From Session ");
                SQL.AppendLine(" Where Active = 1 ");
                SQL.AppendLine(" And DateDiff(MI, DateStart, GetDate()) <= " + Timeout);
                SQL.AppendLine(" And Session.SessionHash Like '" + SessionHash.Replace("'", "''") + "'");

                DataTable data = Functions.Conn.GetDataTable(SQL.ToString(), "Session");
                if (data.Rows.Count > 0)
                {
                    DataRow row = data.AsEnumerable().First();
                    SQL = new StringBuilder();
                    SQL.AppendLine(" Update Session Set ");
                    SQL.AppendLine("    DateStart = GetDate(), ");
                    SQL.AppendLine("    DateEnd = DateAdd(MI," + Timeout + ",GetDate()) ");
                    SQL.AppendLine(" Where SessionId = " + row.Field<long>("SessionId"));

                    Functions.Conn.Execute(SQL.ToString());
                }
                else
                {
                    throw new InvalidOperationException("Não foram encontradas sessões para esse usuário!");
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateSession()
        {
            try
            {
                SQL = new StringBuilder();
                SQL.AppendLine(" Update Session Set ");
                SQL.AppendLine("    Active = 0 ");
                SQL.AppendLine(" Where Active = 1 ");
                SQL.AppendLine(" And DateDiff(MI,DateStart,GetDate()) > " + Timeout);

                Functions.Conn.Update(SQL.ToString());

            }
            catch (Exception ex)
            {

            }
        }

        public static Companys GetCompanySession()
        {
            try
            {
                if (string.IsNullOrEmpty(SessionHash))
                {
                    throw new InternalProgramException("Informe a sessão!");
                }

                DataTable data;

                SQL = new StringBuilder();
                SQL.AppendLine(" Select ");
                SQL.AppendLine("    UserId ");
                SQL.AppendLine(" From Session ");
                SQL.AppendLine(" Where Active = 1 ");
                SQL.AppendLine(" And SessionHash Like '" + SessionHash + "'");

                data = Functions.Conn.GetDataTable(SQL.ToString(), "Session");

                if (data != null && data.Rows.Count > 0)
                {                    
                    return new CompanyDomain().Get<Companys>(CompanyDomain.Type.User, 
                        new UsersDomain().Get(data.AsEnumerable().First().Field<long>("UserId")).UserId);                    
                }
                throw new InternalProgramException("Empresa não encontrada!");                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Sessions GetSessionById(long SessionId)
        {
            try
            {
                if (SessionId <= 0) { throw new InternalProgramException("Informe o código da sessão."); }

                SQL = new StringBuilder();
                SQL.AppendLine(" Select ");
                SQL.AppendLine("    SessionId, ");
                SQL.AppendLine("    UserId, ");
                SQL.AppendLine("    SessionHash, ");
                SQL.AppendLine("    DateStart, ");
                SQL.AppendLine("    DateEnd, ");
                SQL.AppendLine("    Active, ");
                SQL.AppendLine("    DateInsert, ");
                SQL.AppendLine("    DateUpdate ");
                SQL.AppendLine(" From Session ");
                SQL.AppendLine(" Where Active = 1 ");
                SQL.AppendLine(" And Session.SessionId = " + SessionId);

                var data = Functions.Conn.GetDataTable(SQL.ToString(), "Session");
                if (data != null && data.Rows.Count > 0)
                {
                    Sessions session = new Sessions();
                    DataRow row = data.AsEnumerable().First();
                    session.SessionId = row.Field<long>("SessionId");
                    session.UserId = row.Field<long>("UserId");
                    session.SessionHash = row.Field<string>("SessionHash");
                    session.DateStart = row.Field<DateTime>("DateStart").ToString("dd-MM-yyyy"); ;
                    session.DateEnd = row.Field<DateTime>("DateEnd").ToString("dd-MM-yyyy");
                    session.Active = row.Field<bool>("Active");
                    session.DateInsert = row.Field<DateTime>("DateInsert").ToString("dd-MM-yyyy");
                    session.DateUpdate = row.Field<DateTime>("DateUpdate").ToString("dd-MM-yyyy");
                    return session;
                }
                throw new InternalProgramException("Não foi encontrar sessões!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Sessions NewSession(long UserId)
        {
            try
            {
                if (UserId <= 0)
                {
                    throw new InternalProgramException("Usuario não encontrado!");
                }

                Users users = new UsersDomain().Get(UserId);
                if (users.UserId <= 0)
                {
                    throw new InternalProgramException("Usuario não cadastrado na base de dados!");
                }

                DataTable data;

                SQL = new StringBuilder();
                SQL.AppendLine(" Select Top 1 ");
                SQL.AppendLine("    SessionId ");
                SQL.AppendLine(" From Session ");
                SQL.AppendLine(" Where Active = 1 ");
                SQL.AppendLine(" And DateDiff(MI, DateStart, Getdate()) <= " + Timeout);
                SQL.AppendLine(" And Session.UserId = " + UserId);

                data = Functions.Conn.GetDataTable(SQL.ToString(), "Session");
                if (data != null && data.Rows.Count > 0 && data.AsEnumerable().First().Field<long>("SessionId") > 0)
                {
                    return SessionDomain.GetSessionById(data.AsEnumerable().First().Field<long>("SessionId"));
                }

                string Hash = SessionDomain.GenerateHash(users.UserId.ToString() + users.Password.ToString());

                SQL = new StringBuilder();
                SQL.AppendLine(" Insert Into Session ");
                SQL.AppendLine("    (UserId, ");
                SQL.AppendLine("     SessionHash,");
                SQL.AppendLine("     DateStart, ");
                SQL.AppendLine("     DateEnd, ");
                SQL.AppendLine("     Active, ");
                SQL.AppendLine("     DateInsert, ");
                SQL.AppendLine("     DateUpdate)");
                SQL.AppendLine(" Values ");
                SQL.AppendLine("    ( " + UserId + ",");
                SQL.AppendLine("     '" + Hash + "',");
                SQL.AppendLine("     GetDate(),");
                SQL.AppendLine("     DateAdd(MI,"+Timeout+",GetDate()),");
                SQL.AppendLine("     1, ");
                SQL.AppendLine("     GetDate(), ");
                SQL.AppendLine("     GetDate()) ");

                Sessions session = new Sessions();
                session.SessionId = Functions.Conn.Insert(SQL.ToString());

                if (session.SessionId > 0)
                {
                    return SessionDomain.GetSessionById(session.SessionId);
                }
                throw new InternalProgramException("Não foi possivel criar uma sessão para o usuário!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
