using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class CursoAdapter : Adapter
    {
        public List<Curso> GetAll()
        {
            List<Curso> cursos = new List<Curso>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdCurso = new SqlCommand("select * from dbo.cursos cur" +
                                                     " inner join dbo.comisiones com on cur.id_comision = com.id_comision" +
                                                     " inner join dbo.materias mat on cur.id_materia = mat.id_materia" +
                                                     " where cur.baja_logica = 0 and com.baja_logica = 0 and mat.baja_logica = 0", SqlConn);

                SqlDataReader drCursos = cmdCurso.ExecuteReader();

                while (drCursos.Read())
                {
                    Curso c = new Curso();
                    c.ID = (int)drCursos["id_curso"];
                    c.IDMateria = (int)drCursos["id_materia"];
                    c.IDComision = (int)drCursos["id_comision"];
                    c.Cupo = (int)drCursos["cupo"];
                    c.AnioCalendario = (int)drCursos["anio_calendario"];
                }

                drCursos.Close();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Cursos", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return cursos;
        }

        public Curso GetOne(int mate, int comi)
        {
            Curso c = new Curso();

            try
            {
                OpenConnection();

                SqlCommand cmdCurso = new SqlCommand("select * from dbo.cursos where @mate = id_materia and @comi = id_comision and baja_logica = 0", SqlConn);

                cmdCurso.Parameters.AddWithValue("@mate", mate);
                cmdCurso.Parameters.AddWithValue("@comi", comi);

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
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos del Curso", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
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
                OpenConnection();

                SqlCommand cmdSave = new SqlCommand(@"INSERT INTO cursos
                                                        (id_materia, id_comision, anio_calendario, cupo)
                                                       VALUES
                                                       (@materia, @comision, @anio,@cupo) select @@identity", SqlCon);

                cmdSave.Parameters.AddWithValue("@materia", c.IDMateria);
                cmdSave.Parameters.AddWithValue("@comision", c.IDComision);
                cmdSave.Parameters.AddWithValue("@anio", c.AnioCalendario);
                cmdSave.Parameters.AddWithValue("@cupo", c.Cupo);
               

                c.ID = decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear el Curso", ex);
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
                OpenConnection();

                SqlCommand cmdSave = new SqlCommand(@"UPDATE cursos SET 
                                                    id_materia = @materia, id_comision = @comision ,anio_calendario = @anio, 
                                                    cupo = @cupo 
                                                    WHERE id_curso = @id", SqlCon);

                cmdSave.Parameters.AddWithValue("@materia", c.IDMateria);
                cmdSave.Parameters.AddWithValue("@comision", c.IDComision);
                cmdSave.Parameters.AddWithValue("@anio", c.AnioCalendario);
                cmdSave.Parameters.AddWithValue("@cupo", c.Cupo);
                cmdSave.Parameters.AddWithValue("@id", c.ID);

                cmdSave.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar el Curso", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Delete(int id)
        {
            try
            {
                OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE FROM cursos WHERE id_curso = @id", SqlCon);
                cmdDelete.Parameters.AddWithValue("@id", id);
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar el Curso", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }



    }
}

