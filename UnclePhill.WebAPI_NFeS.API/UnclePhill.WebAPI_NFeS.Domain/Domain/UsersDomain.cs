﻿using System;
using System.Data;
using System.Linq;
using System.Text;
using UnclePhill.WebAPI_NFeS.Models;
using UnclePhill.WebAPI_NFeS.Models.Models;
using UnclePhill.WebAPI_NFeS.Utils.Utils;
using static UnclePhill.WebAPI_NFeS.Utils.Utils.Functions;

namespace UnclePhill.WebAPI_NFeS.Domain
{
    public class UsersDomain : MasterDomain
    {      
        public Users Login(string Email, string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    throw new Exception("Email ou senha não informado!");
                }

                SQL = new StringBuilder();
                SQL.AppendLine(" Select * From Users ");
                SQL.AppendLine(" Where Active = 1 ");
                SQL.AppendLine(" And Email Like '" + Functions.NotQuote(Email) + "'");
                SQL.AppendLine(" And Password Like '" + Functions.Encript(Functions.NotQuote(Password)) + "'");

                DataTable data = Functions.Conn.GetDataTable(SQL.ToString(), "Users");
                if (data != null && data.Rows.Count > 0)
                {
                    DataRow row = data.AsEnumerable().First();
                    Sessions session = SessionDomain.NewSession(row.Field<long>("UserId"));
                    if (session.SessionId > 0)
                    {
                        Users users = new Users();
                        users.UserId = row.Field<long>("UserId");
                        users.Name = row.Field<string>("Name");
                        users.LastName = row.Field<string>("LastName");
                        users.CPF = row.Field<string>("CPF");
                        users.Email = row.Field<string>("Email");
                        users.Password = Functions.Desencript(row.Field<string>("Password"));
                        users.SessionHash = session.SessionHash;
                        users.Active = row.Field<bool>("Active");
                        users.DateInsert = row.Field<DateTime>("DateInsert").ToString("dd-MM-yyyy");
                        users.DateUpdate = row.Field<DateTime>("DateUpdate").ToString("dd-MM-yyyy");
                        return users;
                    }
                    throw new Exception("Não foi possivel gerar uma sessão para o usuário!");
                }

                throw new Exception("Email ou senha inválidos!");
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public Users Get(long? UserId)
        {
            try
            {
                if (UserId == null || UserId <= 0)
                {
                    throw new Exception("Informe o código do usuário!");
                }

                SQL = new StringBuilder();
                SQL.AppendLine(" Select * From Users ");
                SQL.AppendLine(" Where Active = 1 ");
                SQL.AppendLine(" And UserId = " + UserId);

                DataTable data = Functions.Conn.GetDataTable(SQL.ToString(), "Users");
                if (data != null && data.Rows.Count > 0)
                {
                    DataRow row = data.AsEnumerable().First();
                    
                    Users users = new Users();
                    users.UserId = row.Field<long>("UserId");
                    users.Name = row.Field<string>("Name");
                    users.LastName = row.Field<string>("LastName");
                    users.CPF = row.Field<string>("CPF");
                    users.Email = row.Field<string>("Email");
                    users.Password = Functions.Desencript(row.Field<string>("Password"));
                    users.SessionHash = string.Empty;
                    users.Active = row.Field<bool>("Active");
                    users.DateInsert = row.Field<DateTime>("DateInsert").ToString("dd-MM-yyyy");
                    users.DateUpdate = row.Field<DateTime>("DateUpdate").ToString("dd-MM-yyyy");
                    return users;                    
                }
                throw new Exception("Não foi encontrado usuário!");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Users Post(Users users)
        {
            try
            {
                Validate(users);

                SQL = new StringBuilder();
                SQL.AppendLine(" Insert Into Users ");
                SQL.AppendLine("   (Name, ");
                SQL.AppendLine("    LastName, ");
                SQL.AppendLine("    CPF, ");
                SQL.AppendLine("    Email, ");
                SQL.AppendLine("    Password, ");
                SQL.AppendLine("    Active, ");
                SQL.AppendLine("    DateInsert, ");
                SQL.AppendLine("    DateUpdate) ");
                SQL.AppendLine(" Values( ");
                SQL.AppendLine("    '" + Functions.NotQuote(users.Name) + "',");
                SQL.AppendLine("    '" + Functions.NotQuote(users.LastName) + "',");
                SQL.AppendLine("    '" + Functions.NotQuote(users.CPF) + "',");
                SQL.AppendLine("    '" + Functions.NotQuote(users.Email) + "',");
                SQL.AppendLine("    '" + Functions.Encript(Functions.NotQuote(users.Password)) + "',");
                SQL.AppendLine("    1,");
                SQL.AppendLine("    GetDate(),");
                SQL.AppendLine("    GetDate() ");
                SQL.AppendLine(" ) ");

                users.UserId = Functions.Conn.Insert(SQL.ToString());
                Sessions session = SessionDomain.NewSession(users.UserId);
                if (users.UserId > 0 && session.SessionId > 0)
                {
                    users.SessionHash = session.SessionHash;
                    users.Active = true;
                    users.DateInsert = DateTime.Now.ToString("yyyy-MM-dd");
                    users.DateUpdate = DateTime.Now.ToString("yyyy-MM-dd");
                    return users;
                }
                throw new Exception("Houve um problema ao cadastrar o usuário.");
            }catch(Exception ex)
            {
                throw ex;
            }           
        }

        public Users Put(Users users)
        {
            try
            {
                if (users.UserId <= 0)
                {
                    throw new Exception("Informe o código do usuário!");
                }

                Validate(users);

                SQL = new StringBuilder();
                SQL.AppendLine(" Update Users Set ");
                SQL.AppendLine("    Name = '" + Functions.NotQuote(users.Name) + "',");
                SQL.AppendLine("    LastName = '" + Functions.NotQuote(users.LastName) + "',");
                SQL.AppendLine("    CPF = '" + Functions.NotQuote(users.CPF) + "',");
                SQL.AppendLine("    Email = '" + Functions.NotQuote(users.Email) + "',");
                SQL.AppendLine("    Password = '" + Functions.Encript(Functions.NotQuote(users.Password)) + "',");
                SQL.AppendLine("    DateUpdate = GetDate() ");
                SQL.AppendLine(" Where Active = 1 ");
                SQL.AppendLine(" And UserId = " + users.UserId);

                if (Functions.Conn.Update(SQL.ToString()))
                {
                    return users;
                }
                throw new Exception("Houve um problema ao atualizar o usuário.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        private void Validate(Users users)
        {
            if (string.IsNullOrEmpty(users.Name))
            {
                throw new Exception("Informe o nome do usuário!");
            }

            if (string.IsNullOrEmpty(users.LastName))
            {
                 throw new Exception("Informe o Sobrenome do usuário!");
            }

            if (string.IsNullOrEmpty(users.CPF))
            {
                 throw new Exception("Informe o CPF do usuário!");
            }

            if (Functions.ExistsRegister(users.CPF,TypeInput.Texto, "CPF", "Users"))
            {
                throw new Exception("Usuário já existe!");
            }

            if (string.IsNullOrEmpty(users.Email))
            {
                 throw new Exception("Informe o Email do usuário!");
            }

            if (string.IsNullOrEmpty(users.Password))
            {
                 throw new Exception("Informe a senha do usuário!");
            }
        }

        
    }
}