using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Database
{
    public class DocenteCursoAdapter:Adapter
    {

        public List<DocenteCurso> GetAll(int ID)
        {
            List<DocenteCurso> docCur = new List<DocenteCurso>();
            try
            {
                this.OpenConnection();
                string query = "";

                SqlCommand cmdDocCur = new SqlCommand(@"SELECT *
                                                        FROM docentes_cursos
                                                        WHERE id_docente = @id", sqlConn);
                cmdDocCur.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drDocCur = cmdDocCur.ExecuteReader();

                while (drDocCur.Read())
                {
                    DocenteCurso dc = new DocenteCurso();
                    dc.ID = (int)drDocCur["id_dictado"];
                    dc.IDCurso = (int)drDocCur["id_curso"];
                    dc.IDDocente = (int)drDocCur["id_docente"];
                    dc.Cargo = (DocenteCurso.TipoCargo)drDocCur["cargo"];
                    docCur.Add(dc);
                }

                drDocCur.Close();
                
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Curos del docente de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return docCur;
        }

        public List<DocenteCursoComplete> GetAllComplete()
        {
            List<DocenteCursoComplete> docCur = new List<DocenteCursoComplete>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdDocCur = new SqlCommand(@"select dc.id_dictado, 
                                                                dc.id_curso, 
                                                                com.desc_comision, 
                                                                m.desc_materia,
                                                                c.anio_calendario,
                                                                dc.id_docente,
                                                                p.nombre,
                                                                p.apellido,
                                                                dc.cargo,
                                                                com.anio_especialidad
                                                        from docentes_cursos dc
                                                        join personas p on dc.id_docente = p.id_persona
                                                        join cursos c on dc.id_curso = c.id_curso
                                                        join comisiones com on c.id_comision = com.id_comision
                                                        join materias m on c.id_materia = m.id_materia;", sqlConn);

                SqlDataReader drDocCur = cmdDocCur.ExecuteReader();

                while (drDocCur.Read())
                {
                    DocenteCursoComplete dc = new DocenteCursoComplete();
                    dc.ID = (int)drDocCur["id_dictado"];
                    dc.IDCurso = (int)drDocCur["id_curso"];
                    dc.DComision = (string)drDocCur["desc_comision"];
                    dc.DMateria = (string)drDocCur["desc_materia"];
                    dc.AnioCalendario = (int)drDocCur["anio_calendario"];
                    dc.AnioEspecialidad = (int)drDocCur["anio_especialidad"];
                    dc.IDDocente = (int)drDocCur["id_docente"];
                    dc.Nombre = (string)drDocCur["nombre"];
                    dc.Apellido = (string)drDocCur["apellido"];
                    dc.Cargo = (DocenteCursoComplete.TipoCargo)drDocCur["cargo"];
                    docCur.Add(dc);
                }

                drDocCur.Close();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Curos del docente de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return docCur;
        }


        public DocenteCurso GetOne(int ID)
        {
            DocenteCurso dc = new DocenteCurso();
            try
            {
                this.OpenConnection();
                SqlCommand cmdDocCur = new SqlCommand(@"SELECT *
                                                        FROM docentes_cursos
                                                        WHERE id_dictado = @id", sqlConn);
                cmdDocCur.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drDocCur = cmdDocCur.ExecuteReader();
                if (drDocCur.Read())
                {
                    dc.ID = (int)drDocCur["id_dictado"];
                    dc.IDCurso = (int)drDocCur["id_curso"];
                    dc.IDDocente = (int)drDocCur["id_docente"];
                    dc.Cargo = (DocenteCurso.TipoCargo)drDocCur["cargo"];
                }
                drDocCur.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos del Curso del docente de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            
            return dc;
        }
        public void Save(DocenteCurso dc)
        {
            if (dc.State == BusinessEntity.States.Deleted)
            {
                this.Delete(dc.ID);
            }
            else if (dc.State == BusinessEntity.States.New)
            {
                this.Insert(dc);
            }
            else if (dc.State == BusinessEntity.States.Modified)
            {
                this.Update(dc);
            }
            dc.State = BusinessEntity.States.Unmodified;
        }

        protected void Insert(DocenteCurso dc)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"INSERT INTO docentes_cursos  
                                                        (id_curso, id_docente, cargo)
                                                    VALUES
                                                        (@id_curso, @id_docente, @cargo)
                                                    SELECT @@identity"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = dc.IDCurso;
                cmdSave.Parameters.Add("@id_docente", SqlDbType.Int).Value = dc.IDDocente;
                cmdSave.Parameters.Add("@cargo", SqlDbType.Int).Value = (int)dc.Cargo;

                dc.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); 

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear docente_curso en la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(DocenteCurso dc)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"UPDATE docentes_cursos SET 
                                                        id_curso = @id_curso,
                                                        id_docente = @id_docente,
                                                        cargo = @cargo
                                                    WHERE id_dictado = @id"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = dc.ID;
                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = dc.IDCurso;
                cmdSave.Parameters.Add("@id_docente", SqlDbType.Int).Value = dc.IDDocente;
                cmdSave.Parameters.Add("@cargo", SqlDbType.Int).Value = (int)dc.Cargo;

                cmdSave.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar docente_curso en la DB", ex);
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
                SqlCommand cmdDelete = new SqlCommand("DELETE FROM docentes_cursos WHERE id_dictado = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar el docente_curso de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }


        }

        
    }
}
