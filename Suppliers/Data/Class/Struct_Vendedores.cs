﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2.Class
{
    public class Struct_Vendedores
    {

        public int Id;
        public int IdUser;
        public string NombreVendedor;
        public decimal Porcentaje;
        

        public Struct_Vendedores(DataRow p_DR) 
        {
            Id = int.Parse(p_DR["Id"].ToString());
            IdUser = int.Parse(p_DR["IdUser"].ToString());
            NombreVendedor = p_DR["NombreVendedor"].ToString();
            Porcentaje = Statics.Conversion.GetDecimal(p_DR["PorcentajeComision"].ToString());
        }
        public static int loginseller(int iduser, string seller, string pass)
        {
            return new Connection.D_Vendedores().logme(iduser, seller, pass);
        }

        public static Struct_Vendedores Insert_Vendedor(string NombreV, int IDUser, decimal Porcent)
        {
            Connection.D_Vendedores D = new Connection.D_Vendedores();
            D.Insert_Vendedor(NombreV, IDUser, Porcent);
            List<Struct_Vendedores>  VL = GetAllVendedores(IDUser);
            if (VL != null)
            {
                return VL[VL.Count - 1];
            }
            else 
            {
                return null;
            }
            
        }

        public static bool UpdatePassword(int p_IdUser, int p_IdVendedor, string p_newPassword)
        {
            Connection.D_Vendedores D = new Connection.D_Vendedores();
            return D.UpdatePassword(p_IdVendedor, p_IdUser, p_newPassword);
        }

        public bool Modify() 
        {
            Connection.D_Vendedores D = new Connection.D_Vendedores();
            return D.Update_Vendedor(NombreVendedor, IdUser, Id, Porcentaje);
        }

        public bool Delete()
        {
            Connection.D_Vendedores D = new Connection.D_Vendedores();
            return D.Delete_Vendedor(Id,IdUser);
        }

        public static Struct_Vendedores Get_VendedorById(int IdVendedor)
        {
            Connection.D_Vendedores V = new Connection.D_Vendedores();
            if (V.get_vendedorById(IdVendedor)!= null){ 
            Struct_Vendedores mv = new Struct_Vendedores(V.get_vendedorById(IdVendedor));
                return mv;
            } else 

{
                return null;
            }

            
        }

        public static List<Struct_Vendedores> GetAllVendedores(int p_IdUser) 
        {
            Connection.D_Vendedores _Conn = new Connection.D_Vendedores();
            DataTable _DT = _Conn.Get_All_Vendedores(p_IdUser);
            if (_DT != null)
            {
                List<Struct_Vendedores> VendedoresList = new List<Struct_Vendedores>();

                for (int a = 0; a < _DT.Rows.Count; a++)
                {
                    VendedoresList.Add(new Struct_Vendedores(_DT.Rows[a]));
                }
                return VendedoresList;
            }
            else 
            {
                return null;
            }
        }

    }
}
