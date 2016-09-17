using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Database
{
    public class PlanAdapter:Adapter
    {

        public List<Plan> GetAll()
        {
            List<Plan> planes = new List<Plan>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdPlanes = new SqlCommand(@"SELECT  p.id_plan, p.desc_plan, e.id_especialidad, e.desc_especialidad 
                                                        FROM planes AS p
                                                        JOIN especialidades AS e 
                                                            ON p.id_especialidad = e.id_especialidad", sqlConn);

                SqlDataReader drPlanes = cmdPlanes.ExecuteReader();

                while (drPlanes.Read())
                {
                    Plan p = new Plan();
                    p.ID = (int)drPlanes["id_plan"];
                    p.Descripcion = (string)drPlanes["desc_plan"];
                    p.IDEspecialidad = (int)drPlanes["id_especialidad"];
                    planes.Add(p);
                }

                drPlanes.Close();
                
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Planes", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return planes;
        }
        public Plan GetOne(int ID)
        {
            Plan p = new Plan();
            try
            {
                this.OpenConnection();
                SqlCommand cmdPlanes= new SqlCommand("SELECT * FROM planes WHERE id_plan= @id", sqlConn);
                cmdPlanes.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drPlanes = cmdPlanes.ExecuteReader();
                if (drPlanes.Read())
                {
                    p.ID = (int)drPlanes["id_plan"];
                    p.Descripcion = (string)drPlanes["desc_plan"];
                    p.IDEspecialidad= (int)drPlanes["id_especialidad"];
                }
                drPlanes.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos del Plan", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            
            return p;
        }
        public void Save(Plan p)
        {
            if (p.State == BusinessEntity.States.Deleted)
            {
                this.Delete(p.ID);
            }
            else if (p.State == BusinessEntity.States.New)
            {
                this.Insert(p);
            }
            else if (p.State == BusinessEntity.States.Modified)
            {
                this.Update(p);
            }
            p.State = BusinessEntity.States.Unmodified;
        }

        protected void Insert(Plan p)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"INSERT INTO planes  
                                                        (desc_plan, id_especialidad)
                                                    VALUES
                                                        (@desc_plan, @id_especialidad)
                                                    SELECT @@indentity"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@desc_modulo", SqlDbType.VarChar, 50).Value = p.Descripcion;
                cmdSave.Parameters.Add("@id_especialidad", SqlDbType.Int ).Value = p.IDEspecialidad;

                p.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); 

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear Plan", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(Plan p)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"UPDATE planes SET 
                                                        desc_plan = @desc_plan,
                                                        id_especialidad = @id_especialidad
                                                    WHERE id_plan = @id"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = p.ID;
                cmdSave.Parameters.Add("@desc_plan", SqlDbType.VarChar, 50).Value = p.Descripcion;
                cmdSave.Parameters.Add("@id_especialidad", SqlDbType.Int).Value = p.IDEspecialidad;

                cmdSave.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar el Plan", ex);
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
                SqlCommand cmdDelete = new SqlCommand("DELETE FROM planes WHERE id_plan = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar el Plan", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }


        }

        
    }
}
