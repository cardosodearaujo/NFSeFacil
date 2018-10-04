﻿using System;
using System.Web.Mvc;
using UnclePhill.WebAPI_NFeS.API.Controllers;
using UnclePhill.WebAPI_NFeS.Models;
using UnclePhill.WebAPI_NFeS.Domain;
using System.Web.Http;
using UnclePhill.WebAPI_NFeS.Models.Models;
using System.Web.Http.Cors;

namespace UnclePhill.WebAPI_NFeS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] 
    public class ShippingCompanyController : MasterController, Default.IController<ShippingCompany>
    {
        ShippingCompanyDomain shippingCompanyDomain = new ShippingCompanyDomain();

        /// <summary>
        /// Retorna uma lista as transportadoras
        /// </summary>
        /// <param name="SessionHash">Paramentro passado no Header da requisição</param>
        /// <param name="ShippingCompanyId">Opcional: Código da transportadora</param>
        /// <returns code = "200">Sucesso</returns>
        /// <returns code = "400">Erro</returns> 
        public IHttpActionResult Get(long? ShippingCompanyId)
        {
            try
            {
                if (!SessionDomain.CheckSession(Sessao())) { return BadRequest("Sessão inválida!"); }

                return Ok(shippingCompanyDomain.Get(ShippingCompanyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria uma transportadora
        /// </summary>
        /// <param name="SessionHash">Paramentro passado no Header da requisição</param>
        /// <param name="ShippingCompany">Objeto transportadora</param>
        /// <returns code = "200">Sucesso</returns>
        /// <returns code = "400">Erro</returns> 
        public IHttpActionResult Post([FromBody]ShippingCompany ShippingCompany)
        {
            try
            {
                if (!SessionDomain.CheckSession(Sessao())) { return BadRequest("Sessão inválida!"); }

                if (shippingCompanyDomain.Post(ShippingCompany))
                {
                    return Ok("Transportadora criada com sucesso!");
                }

                return BadRequest("Houve um problema ao criar a transportadora. Tente novamente!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza uma transportadora
        /// </summary>
        /// <param name="SessionHash">Paramentro passado no Header da requisição</param>
        /// <param name="ShippingCompany">Objeto transportadora</param>
        /// <returns code = "200">Sucesso</returns>
        /// <returns code = "400">Erro</returns>
        public IHttpActionResult Put([FromBody]ShippingCompany ShippingCompany)
        {
            try
            {
                if (!SessionDomain.CheckSession(Sessao())) { return BadRequest("Sessão inválida!"); }

                if (shippingCompanyDomain.Put(ShippingCompany))
                {
                    return Ok("Serviço atualizado com sucesso!");
                }

                return BadRequest("Houve um erro ao atualizar o serviço. Tente novamente!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma transoportadora pelo Id
        /// </summary>
        /// <param name="SessionHash">Paramentro passado no Header da requisição</param>
        /// <param name="ShippingCompanyId">Id da transportadora</param>
        /// <returns code = "200">Sucesso</returns>
        /// <returns code = "400">Erro</returns>
        public IHttpActionResult Delete(long ShippingCompanyId)
        {
            try
            {
                if (!SessionDomain.CheckSession(Sessao())) { return BadRequest("Sessão inválida!"); }

                if (shippingCompanyDomain.Delete(ShippingCompanyId))
                {
                    return Ok("Transportadora excluida com sucesso!");
                }

                return BadRequest("Houve um erro ao excluir a transportadora. Tente novamente!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
    }
}