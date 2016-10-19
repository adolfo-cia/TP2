using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Database
{
    public class AlumnoInscripcionAdapter:Adapter
    {

        public List<AlumnoInscripcion> GetAll(int? ID = null)
        {
            List<AlumnoInscripcion> inscripciones = new List<AlumnoInscripcion>();
            try
            {
                this.OpenConnection();
                string query = "";

                SqlCommand cmdInscripciones = new SqlCommand();
                cmdInscripciones.Connection = sqlConn;

                if (ID != null)
                {
                    query = @"SELECT *
                            FROM alumnos_inscripciones
                            WHERE id_alumno = @id";
                    cmdInscripciones.CommandText = query;
                    cmdInscripciones.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                }
                else
                {
                    query = @"SELECT *
                            FROM alumnos_inscripciones";
                    cmdInscripciones.CommandText = query;
                }


                SqlDataReader drInscripciones = cmdInscripciones.ExecuteReader();

                while (drInscripciones.Read())
                {
                    AlumnoInscripcion ai = new AlumnoInscripcion();
                    ai.ID = (int)drInscripciones["id_inscripcion"];
                    ai.IDAlumno = (int)drInscripciones["id_alumno"];
                    ai.IDCurso = (int)drInscripciones["id_curso"];
                    ai.Condicion = (string)drInscripciones["condicion"];
                    ai.Nota = (int)drInscripciones["nota"];
                    inscripciones.Add(ai);
                }

                drInscripciones.Close();
                
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Inscripciones de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return inscripciones;
        }

        //public List<AlumnoInscripcion> GetAllCursos()
        //{
        //    List<AlumnoInscripcion> inscripciones = new List<AlumnoInscripcion>();
        //    try
        //    {
        //        this.OpenConnection();

        //        SqlCommand cmdInscripciones = new SqlCommand(@"SELECT *
        //                                                    FROM alumnos_inscripciones", sqlConn);

        //        SqlDataReader drInscripciones = cmdInscripciones.ExecuteReader();

        //        while (drInscripciones.Read())
        //        {
        //            AlumnoInscripcion ai = new AlumnoInscripcion();
        //            ai.ID = (int)drInscripciones["id_inscripcion"];
        //            ai.IDAlumno = (int)drInscripciones["id_alumno"];
        //            ai.IDCurso = (int)drInscripciones["id_curso"];
        //            ai.Condicion = (string)drInscripciones["condicion"];
        //            ai.Nota = (int)drInscripciones["nota"];
        //            inscripciones.Add(ai);
        //        }

        //        drInscripciones.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        Exception ExcepcionManejada = new Exception("Error al recuperar lista de Inscripciones de la DB", ex);
        //        throw ExcepcionManejada;
        //    }
        //    finally
        //    {
        //        this.CloseConnection();
        //    }
        //    return inscripciones;
        //}

        public AlumnoInscripcion GetOne(int ID)
        {
            AlumnoInscripcion inscripcion = new AlumnoInscripcion();
            try
            {
                this.OpenConnection();
                SqlCommand cmdInscripcion = new SqlCommand(@"SELECT *
                                                            FROM alumnos_inscripciones
                                                        WHERE id_inscripcion = @id", sqlConn);
                cmdInscripcion.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drInscripcion = cmdInscripcion.ExecuteReader();
                if (drInscripcion.Read())
                {
                    inscripcion.ID = (int)drInscripcion["id_inscripcion"];
                    inscripcion.IDAlumno = (int)drInscripcion["id_alumno"];
                    inscripcion.IDCurso = (int)drInscripcion["id_curso"];
                    inscripcion.Condicion = (string)drInscripcion["condicion"];
                    inscripcion.Nota = (int)drInscripcion["nota"];
                }
                drInscripcion.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de la Inscripcion de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            
            return inscripcion;
        }
        public void Save(AlumnoInscripcion ai)
        {
            if (ai.State == BusinessEntity.States.Deleted)
            {
                this.Delete(ai.ID);
            }
            else if (ai.State == BusinessEntity.States.New)
            {
                this.Insert(ai);
            }
            else if (ai.State == BusinessEntity.States.Modified)
            {
                this.Update(ai);
            }
            ai.State = BusinessEntity.States.Unmodified;
        }

        protected void Insert(AlumnoInscripcion ai)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"INSERT INTO alumnos_inscripciones  
                                                        (id_alumno, id_curso, condicion, nota)
                                                    VALUES
                                                        (@id_alumno, @id_curso, @condicion, @nota)
                                                    SELECT @@identity"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@id_alumno", SqlDbType.Int).Value = ai.IDAlumno;
                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = ai.IDCurso;
                cmdSave.Parameters.Add("@condicion", SqlDbType.VarChar, 50 ).Value = ai.Condicion;
                cmdSave.Parameters.Add("@nota", SqlDbType.Int).Value = ai.Nota;

                ai.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); 

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear Inscripcion en la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(AlumnoInscripcion ai)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"UPDATE alumnos_inscripciones SET 
                                                        id_alumno = @id_alumno,
                                                        id_curso = @id_curso,
                                                        condicion = @condicion,
                                                        nota = @nota
                                                    WHERE id_inscripcion = @id"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = ai.ID;
                cmdSave.Parameters.Add("@id_alumno", SqlDbType.Int).Value = ai.IDAlumno;
                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = ai.IDCurso;
                cmdSave.Parameters.Add("@condicion", SqlDbType.VarChar, 50).Value = ai.Condicion;
                cmdSave.Parameters.Add("@nota", SqlDbType.Int).Value = ai.Nota;

                cmdSave.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar la Inscripcion en la DB", ex);
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
                SqlCommand cmdDelete = new SqlCommand("DELETE FROM alumnos_inscripciones WHERE id_inscripcion = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar la Inscripcion de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }


        }

        
    }
}
