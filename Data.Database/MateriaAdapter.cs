using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Database
{
    public class MateriaAdapter:Adapter
    {

        public List<Materia> GetAll()
        {
            List<Materia> materias = new List<Materia>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdMaterias = new SqlCommand(@"SELECT * FROM materias" 
                                                        , sqlConn);

                SqlDataReader drMaterias = cmdMaterias.ExecuteReader();

                while (drMaterias.Read())
                {
                    Materia m = new Materia();
                    m.ID = (int)drMaterias["id_materia"];
                    m.Descripcion = (string)drMaterias["desc_materia"];
                    m.HSSemanales = (int)drMaterias["hs_semanales"];
                    m.HSSTotales= (int)drMaterias["hs_totales"];
                    m.IDPlan= (int)drMaterias["id_plan"];
                    materias.Add(m);
                }

                drMaterias.Close();
                
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Materias de la db", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return materias;
        }
        public Materia GetOne(int ID)
        {
            Materia m = new Materia();
            try
            {
                this.OpenConnection();
                SqlCommand cmdMateria = new SqlCommand("SELECT * FROM materias WHERE id_materia = @id", sqlConn);
                cmdMateria.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drMaterias = cmdMateria.ExecuteReader();
                if (drMaterias.Read())
                {
                    m.ID = (int)drMaterias["id_materia"];
                    m.Descripcion = (string)drMaterias["desc_materia"];
                    m.HSSemanales = (int)drMaterias["hs_semanales"];
                    m.HSSTotales = (int)drMaterias["hs_totales"];
                    m.IDPlan = (int)drMaterias["id_plan"];
                }
                drMaterias.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de la Materia de la db", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            
            return m;
        }
        public void Save(Materia m)
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
            m.State = BusinessEntity.States.Unmodified;
        }

        protected void Insert(Materia m)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"INSERT INTO materias
                                                        (desc_materia, hs_semanales, hs_totales, id_plan)
                                                    VALUES
                                                        (@desc_materia, @hs_semanales, @hs_totales, @id_plan)
                                                    SELECT @@indentity"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@desc_materia", SqlDbType.VarChar, 50).Value = m.Descripcion;
                cmdSave.Parameters.Add("@hs_semanales", SqlDbType.Int ).Value = m.HSSemanales;
                cmdSave.Parameters.Add("@hs_totales", SqlDbType.Int).Value = m.HSSTotales;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = m.IDPlan;

                m.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); 

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear Materia en la db", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(Materia m)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"UPDATE materias SET 
                                                        desc_materia = @desc_materia,
                                                        hs_semanales = @hs_semanales,
                                                        hs_totales = @hs_totales,
                                                        id_plan = @id_plan
                                                    WHERE id_materia = @id"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = m.ID;
                cmdSave.Parameters.Add("@desc_materia", SqlDbType.VarChar, 50).Value = m.Descripcion;
                cmdSave.Parameters.Add("@hs_semanales", SqlDbType.Int).Value = m.HSSemanales;
                cmdSave.Parameters.Add("@hs_totales", SqlDbType.Int).Value = m.HSSTotales;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = m.IDPlan;

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
                SqlCommand cmdDelete = new SqlCommand("DELETE FROM materias WHERE id_materia = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar la Materia de la db", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }


        }

        
    }
}
