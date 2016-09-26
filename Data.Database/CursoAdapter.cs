using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Database
{
    public class CursoAdapter:Adapter
    {

        public List<Curso> GetAll()
        {
            List<Curso> cursos = new List<Curso>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdCursos = new SqlCommand(@"SELECT  c.id_curso, c.id_materia, c.id_comision, c.anio_calendario, c.cupo
                                                        FROM cursos AS c", sqlConn);

                SqlDataReader drCursos = cmdCursos.ExecuteReader();

                while (drCursos.Read())
                {
                    Curso c = new Curso();
                    c.ID = (int)drCursos["id_curso"];
                    c.IDMateria= (int)drCursos["id_materia"];
                    c.IDComision= (int)drCursos["id_comision"];
                    c.AnioCalendario= (int)drCursos["anio_calendario"];
                    c.Cupo = (int)drCursos["cupo"];
                    cursos.Add(c);
                }

                drCursos.Close();
                
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Cursos de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return cursos;
        }
        public Curso GetOne(int ID)
        {
            Curso c = new Curso();
            try
            {
                this.OpenConnection();
                SqlCommand cmdCurso = new SqlCommand(@"SELECT  c.id_curso, c.id_materia, c.id_comision, c.anio_calendario, c.cupo
                                                        FROM cursos AS c 
                                                        WHERE id_curso= @id", sqlConn);
                cmdCurso.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drCurso = cmdCurso.ExecuteReader();
                if (drCurso.Read())
                {
                    c.ID = (int)drCurso["id_curso"];
                    c.IDMateria = (int)drCurso["id_materia"];
                    c.IDComision = (int)drCurso["id_comision"];
                    c.AnioCalendario = (int)drCurso["anio_calendario"];
                    c.Cupo = (int)drCurso["cupo"];
                }
                drCurso.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos del Curso de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            
            return c;
        }
        public Curso GetOne(int mate, int comi)
        {
            Curso c = new Curso();

            try
            {
                OpenConnection();

                SqlCommand cmdCurso = new SqlCommand(@"SELECT * 
                                                        FROM cursos 
                                                        WHERE @mate = id_materia AND @comi = id_comision", sqlConn);

                cmdCurso.Parameters.Add("@mate", SqlDbType.Int).Value = mate;
                cmdCurso.Parameters.Add("@comi", SqlDbType.Int).Value = comi;
                
                SqlDataReader drCurso = cmdCurso.ExecuteReader();

                if (drCurso.Read())
                {
                    c.ID = (int)drCurso["id_curso"];
                    c.IDMateria = (int)drCurso["id_materia"];
                    c.IDComision = (int)drCurso["id_comision"];
                    c.Cupo = (int)drCurso["cupo"];
                    c.AnioCalendario = (int)drCurso["anio_calendario"];

                    drCurso.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al recuperar datos del Curso de la DB", e);
            }
            finally
            {
                CloseConnection();
            }

            return c;
        }

        public void Save(Curso c)
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

        protected void Insert(Curso c)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"INSERT INTO cursos  
                                                        (id_comision, id_materia, anio_calendario, cupo)
                                                    VALUES
                                                        (@id_comision, @id_materia, @anio_calendario, @cupo)
                                                    SELECT @@indentity"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@id_comision", SqlDbType.Int ).Value = c.IDComision;
                cmdSave.Parameters.Add("@id_materia", SqlDbType.Int).Value = c.IDMateria;
                cmdSave.Parameters.Add("@anio_calendario", SqlDbType.Int).Value = c.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = c.Cupo;

                c.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); 

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear Curso en la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(Curso c)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"UPDATE cursos SET 
                                                        id_materia = @id_materia,
                                                        id_comision = @id_comision,
                                                        anio_calendario = @anio_calendario,
                                                        cupo = @cupo
                                                    WHERE id_curso = @id"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = c.ID;
                cmdSave.Parameters.Add("@id_comision", SqlDbType.Int).Value = c.IDComision;
                cmdSave.Parameters.Add("@id_materia", SqlDbType.Int).Value = c.IDMateria;
                cmdSave.Parameters.Add("@anio_calendario", SqlDbType.Int).Value = c.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = c.Cupo;

                cmdSave.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar el Curso en la DB", ex);
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
                SqlCommand cmdDelete = new SqlCommand("DELETE FROM cursos WHERE id_curso = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar el Curso de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }


        }

        
    }
}
