﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using UnclePhill.WebAPI_NFeS.API.Models;
using UnclePhill.WebAPI_NFeS.WS.Models;

namespace UnclePhill.WebAPI_NFeS.WS.Domain
{
    public class MasterDomain
    {
        protected ConnectionManager Conn = new ConnectionManager("unclephill.database.windows.net", "BD_NFeS", "1433", "Administrador", "M1n3Rv@7");
        protected StringBuilder SQL = new StringBuilder();
        protected Sessions Session = new Sessions();

        protected string GenerateHash(string Password)
        {
            try
            {
                UnicodeEncoding unicode = new UnicodeEncoding();
                byte[] passwordByte = unicode.GetBytes(Password + DateTime.Now.ToString());
                SHA1Managed SHA1 = new SHA1Managed();
                byte[] hashByte = SHA1.ComputeHash(passwordByte);
                string hash = string.Empty;

                foreach (byte b in hashByte)
                {
                    hash += b.ToString();
                }
                return hash;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Funções diversas:
        protected string NoInjection(string Value)
        {
            return Value.Replace("'", "''");
        }

        protected string FormatNumber(decimal Value)
        {
            return Value.ToString().Replace(".", "").Replace(",", ".");
        }
           
        protected bool IsXml(string parameter)
        {
            try
            {
                XmlDocument XML = new XmlDocument();
                XML.LoadXml(parameter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}