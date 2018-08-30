﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnclePhill.WebAPI_NFeS.API.Controllers;
using UnclePhill.WebAPI_NFeS.API.Models;
using UnclePhill.WebAPI_NFeS.WS.Domain;
using UnclePhill.WebAPI_NFeS.WS.Models;

namespace UnclePhill.WebAPI_NFeS.WS.Controllers
{
    public class TakerController : MasterController
    {

        private TakerDomain takerDomain = new TakerDomain();

        [HttpGet]
        public JsonResult Select(long? TakerId = 0)
        {
            try
            {
                if (!base.CheckSession()) return Response(new Feedbacks("erro", "Sessão inválida!"));

                return Response(takerDomain.Select(TakerId));
            }
            catch(Exception ex)
            {
                return Response(new Feedbacks("erro", ex.Message));
            }            
        }

        [HttpPost]
        public JsonResult Insert(Takers takers)
        {
            try
            {
                if (!base.CheckSession()){ return Response(new Feedbacks("erro", "Sessão inválida!"));}

                Feedbacks feedback = Validate(takers);
                if (feedback.Status.Equals("erro"))
                {
                    return Response(feedback);
                }

                SQL.AppendLine(" Insert Into Takers ");
                SQL.AppendLine("    (IM, ");
                SQL.AppendLine("    CPF_CNPJ, ");
                SQL.AppendLine("    RG_IE, ");
                SQL.AppendLine("    Name, ");
                SQL.AppendLine("    NameFantasy, ");
                SQL.AppendLine("    TypePerson, ");
                SQL.AppendLine("    CEP, ");
                SQL.AppendLine("    Street, ");
                SQL.AppendLine("    Neighborhood, ");
                SQL.AppendLine("    City, ");
                SQL.AppendLine("    State, ");
                SQL.AppendLine("    Email, ");
                SQL.AppendLine("    Active, ");
                SQL.AppendLine("    DateInsert, ");
                SQL.AppendLine("    DateUpdate) ");
                SQL.AppendLine(" Values ");
                SQL.AppendLine("    ('" + NoInjection(takers.IM) + "',");
                SQL.AppendLine("     '" + NoInjection(takers.CPF_CNPJ) + "',");
                SQL.AppendLine("     '" + NoInjection(takers.RG_IE) + "',");
                SQL.AppendLine("     '" + NoInjection(takers.Name) + "',");
                SQL.AppendLine("     '" + NoInjection(takers.NameFantasy) + "',");
                SQL.AppendLine("     '" + NoInjection(takers.TypePerson) + "',");
                SQL.AppendLine("     '" + NoInjection(takers.CEP) + "',");
                SQL.AppendLine("     '" + NoInjection(takers.Street) + "',");
                SQL.AppendLine("     '" + NoInjection(takers.Neighborhood) + "',");
                SQL.AppendLine("     '" + NoInjection(takers.City) + "',");
                SQL.AppendLine("     '" + NoInjection(takers.State) + "',");
                SQL.AppendLine("     '" + NoInjection(takers.Email) + "',");
                SQL.AppendLine("     1 ,");
                SQL.AppendLine("     GetDate(), ");
                SQL.AppendLine("     GetDate() ");
                SQL.AppendLine("    ) ");

                if (Conn.Insert(SQL.ToString())> 0)
                {
                    return Response(new Feedbacks("ok", "Tomador criado com sucesso!"));
                }

                return Response(new Feedbacks("erro", "Houve um problema ao criar um tomador. Tente novamente!"));
            }
            catch(Exception ex)
            {
                return Response(new Feedbacks("erro", ex.Message));
            }            
        }

        [HttpPut]
        public JsonResult Update(Takers takers)
        {
            try
            {
                if (!base.CheckSession()) { return Response(new Feedbacks("erro", "Sessão inválida!")); }

                if (takerDomain.Update(takers))
                {
                    return Response(new Feedbacks("ok","Tomador atualizado com sucesso!"));
                }

                return Response(new Feedbacks("erro","Houve um erro ao atualizar o tomador. Tente novamente!"));
            }
            catch (Exception ex)
            {
                return Response(new Feedbacks("erro", ex.Message));
            }
        }

        [HttpDelete]
        public JsonResult Delete(long TakerId)
        {
            try
            {
                if (!base.CheckSession()) { return Response(new Feedbacks("erro", "Sessão inválida!")); }

                if (TakerId <= 0)
                {
                    return Response(new Feedbacks("erro", "Informe o código do tomador!"));
                }

                SQL.AppendLine(" Update Takers Set ");
                SQL.AppendLine("    Active = 0 ");
                SQL.AppendLine(" Where TakerId = " + TakerId);
                
                if (Conn.Delete(SQL.ToString()))
                {
                    return Response(new Feedbacks("ok","Tomador excluido com sucesso!"));
                }

                return Response(new Feedbacks("erro", "Houve um erro ao excluir o tomador. Tente novamente!"));
            }
            catch (Exception ex)
            {
                return Response(new Feedbacks("erro", ex.Message));
            }
        }
        
        private Feedbacks Validate(Takers takers)
        {
            if (string.IsNullOrEmpty(takers.IM))
            {
                return new Feedbacks("erro", "Informe a inscrição municipal!");
            }

            if (string.IsNullOrEmpty(takers.CPF_CNPJ))
            {
                return new Feedbacks("erro", "Informe o CPF/CNPJ!");
            }

            if (string.IsNullOrEmpty(takers.RG_IE))
            {
                return new Feedbacks("erro", "Informe a inscrição estadual!");
            }

            if (string.IsNullOrEmpty(takers.RG_IE))
            {
                return new Feedbacks("erro", "Informe a inscrição estadual!");
            }

            if (string.IsNullOrEmpty(takers.Name))
            {
                return new Feedbacks("erro", "Informe o nome do tomador!");
            }

            if (string.IsNullOrEmpty(takers.NameFantasy))
            {
                return new Feedbacks("erro", "Informe o nome nome fantasia do tomador!");
            }

            if (string.IsNullOrEmpty(takers.TypePerson))
            {
                return new Feedbacks("erro", "Informe o tipo de pessoa do tomador!");
            }

            if (string.IsNullOrEmpty(takers.CEP))
            {
                return new Feedbacks("erro", "Informe o CEP!");
            }

            if (string.IsNullOrEmpty(takers.Street))
            {
                return new Feedbacks("erro", "Informe a Rua!");
            }

            if (string.IsNullOrEmpty(takers.Neighborhood))
            {
                return new Feedbacks("erro", "Informe o bairro!");
            }

            if (string.IsNullOrEmpty(takers.City))
            {
                return new Feedbacks("erro", "Informe a cidade!");
            }

            if (string.IsNullOrEmpty(takers.State))
            {
                return new Feedbacks("erro", "Informe a UF!");
            }

            if (string.IsNullOrEmpty(takers.Email))
            {
                return new Feedbacks("erro", "Informe o CEP!");
            }

            return new Feedbacks("ok", "Sucesso");
        }
    }
}