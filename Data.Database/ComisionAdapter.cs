using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Database
{
    public class ComisionAdapter:Adapter
    {

        public List<Comision> GetAll()
        {
            List<Comision> comisiones = new List<Comision>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdComisiones = new SqlCommand(@"SELECT  c.id_comision, c.desc_comision, c.anio_especialidad, c.id_plan
                                                            FROM comisiones AS c", sqlConn);

                SqlDataReader drComisiones = cmdComisiones.ExecuteReader();

                while (drComisiones.Read())
                {
                    Comision c = new Comision();
                    c.ID = (int)drComisiones["id_comision"];
                    c.Descripcion = (string)drComisiones["desc_comision"];
                    c.AnioEspecialidad = (int)drComisiones["anio_especialidad"];
                    c.IDPlan = (int)drComisiones["id_plan"];
                    comisiones.Add(c);
                }

                drComisiones.Close();
                
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Comisiones de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return comisiones;
        }
        public Comision GetOne(int ID)
        {
            Comision c = new Comision();
            try
            {
                this.OpenConnection();
                SqlCommand cmdComision = new SqlCommand(@"SELECT  c.id_comision, c.desc_comision, c.anio_especialidad, c.id_plan
                                                            FROM comisiones AS c
                                                        WHERE id_comision = @id", sqlConn);
                cmdComision.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drComision = cmdComision.ExecuteReader();
                if (drComision.Read())
                {
                    c.ID = (int)drComision["id_comision"];
                    c.Descripcion = (string)drComision["desc_comision"];
                    c.AnioEspecialidad = (int)drComision["anio_especialidad"];
                    c.IDPlan = (int)drComision["id_plan"];
                }
                drComision.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de la Comision de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            
            return c;
        }
        public void Save(Comision c)
        {
            if (c.State == BusinessEntity.States.Deleted)
            {
                this.Delete(c.ID);
            }
            else if (c.State == BusinessEntity.States.New)
            {
                this.Insert(c);
            }
            else if (c.State == BusinessEntity.States.Modified)
            {
                this.Update(c);
            }
            c.State = BusinessEntity.States.Unmodified;
        }

        protected void Insert(Comision c)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"INSERT INTO comisiones  
                                                        (desc_comision, anio_especialidad, id_plan)
                                                    VALUES
                                                        (@desc_comision, @anio_especialidad, @id_plan)
                                                    SELECT @@indentity"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@desc_comision", SqlDbType.VarChar, 50 ).Value = c.Descripcion;
                cmdSave.Parameters.Add("@anio_especialidad", SqlDbType.Int).Value = c.AnioEspecialidad;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = c.IDPlan;

                c.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); 

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear Comision en la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(Comision c)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"UPDATE comisiones SET 
                                                        desc_comision = @desc_comision,
                                                        anio_especialidad = @anio_especialidad,
                                                        id_plan = @id_plan
                                                    WHERE id_comision = @id"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = c.ID;
                cmdSave.Parameters.Add("@desc_comision", SqlDbType.Int).Value = c.Descripcion;
                cmdSave.Parameters.Add("@anio_especialidad", SqlDbType.Int).Value = c.AnioEspecialidad;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = c.IDPlan;

                cmdSave.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar la Comision en la DB", ex);
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
                SqlCommand cmdDelete = new SqlCommand("DELETE FROM comisiones WHERE id_comision = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar la Comision de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }


        }

        
    }
}
