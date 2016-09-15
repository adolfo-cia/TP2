using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class ModuloAdapter:Adapter
    {

        public List<Modulo> GetAll()
        {
            List<Modulo> modulos = new List<Modulo>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdModulos = new SqlCommand("select * from modulos", sqlConn);

                SqlDataReader drModulos = cmdModulos.ExecuteReader();

                while (drModulos.Read())
                {
                    Modulo m = new Modulo();
                    m.ID = (int)drModulos["id_modulo"];
                    m.Descripcion = (string)drModulos["desc_modulo"];
                    modulos.Add(m);
                }

                drModulos.Close();
                
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Modulos", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return modulos;
        }
        public Modulo GetOne(int ID)
        {
            Modulo m = new Modulo();
            try
            {
                this.OpenConnection();
                SqlCommand cmdModulos = new SqlCommand("select * from modulos where id_modulo = @id", sqlConn);
                cmdModulos.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drModulos = cmdModulos.ExecuteReader();
                if (drModulos.Read())
                {
                    m.ID = (int)drModulos["id_modulo"];
                    m.Descripcion = (string)drModulos["desc_modulo"];
                }
                drModulos.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos del Modulo", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            
            return m;
        }
        public void Save(Modulo m)
        {
            if (m.State == BusinessEntity.States.Deleted)
            {
                this.Delete(m.ID);
            }
            else if (m.State == BusinessEntity.States.New)
            {
                this.Insert(m);
            }
            else if (m.State == BusinessEntity.States.Modified)
            {
                this.Update(m);
            }
            e.State = BusinessEntity.States.Unmodified;
        }

        protected void Insert(Modulo m)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"INSERT INTO modulos  
                                                        (desc_modulo)
                                                    VALUES
                                                        (@desc_modulo)
                                                    SELECT @@indentity"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@desc_modulo", SqlDbType.VarChar, 50).Value = m.Descripcion;
               
                m.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); 

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear Modulo", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(Modulo m)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"UPDATE modulos SET 
                                                        desc_modulo = @desc_modulo
                                                    WHERE id_modulo = @id"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = m.ID;
                cmdSave.Parameters.Add("@desc_modulo", SqlDbType.VarChar, 50).Value = m.Descripcion;
             
                cmdSave.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar el Modulo", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

        }
        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("DELETE FROM modulos WHERE id_modulo = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar el Modulo", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }


        }

        
    }
}
