using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class PersonaAdapter:Adapter
    {
        private SqlTransaction trans;
        //trae todas las personas segun el tipo
        public List<Persona> GetAll(Persona.TipoPersona tipo)
        {


            List<Persona> personas = new List<Persona>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdPersonas = new SqlCommand(@"SELECT * 
                                                        FROM personas
                                                        WHERE tipo_persona = @tipo_persona", sqlConn);
                cmdPersonas.Parameters.Add("@tipo_persona", SqlDbType.VarChar, 50).Value = tipo;
                SqlDataReader drPersonas = cmdPersonas.ExecuteReader();

                while (drPersonas.Read())
                {
                    Persona p = new Persona();
                    p.ID = (int)drPersonas["id_persona"];

                    p.Nombre = (string)((drPersonas["nombre"] != null) ? "" : drPersonas["nombre"]);
                    p.Apellido = (string)((drPersonas["apellido"] != null) ? "" : drPersonas["apellido"]);
                    p.Email = (string)((drPersonas["email"] != null) ? "" : drPersonas["email"]);
                    p.Direccion = (string)((drPersonas["direccion"] != null) ? "" : drPersonas["direccion"]);
                    p.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    p.IDPlan = (int?)((drPersonas["id_plan"] != null) ? null : drPersonas["id_plan"]);
                    p.Legajo = (int?)((drPersonas["legajo"] != null) ? null : drPersonas["legajo"]);
                    p.Telefono = (string)((drPersonas["telefono"] != null) ? "" : drPersonas["telefono"]);
                    p.Tipo = (Persona.TipoPersona)drPersonas["tipo_persona"];
                    p.Clave = (string)drPersonas["clave"];
                    p.Habilitado = (bool)drPersonas["habilitado"];
                    p.NombreUsuario = (string)drPersonas["nombre_usuario"];
                    //p.CambiaClave = (int)((drPersonas["cambia_clave"]!= null) ? null : drPersonas["cambia_clave"]);

                    personas.Add(p);
                }

                drPersonas.Close();
                
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Personas de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return personas;
        }
        public List<Persona> GetAll()
        {


            List<Persona> personas = new List<Persona>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdPersonas = new SqlCommand(@"SELECT * 
                                                        FROM personas", sqlConn);
                SqlDataReader drPersonas = cmdPersonas.ExecuteReader();

                while (drPersonas.Read())
                {
                    Persona p = new Persona();
                    p.ID = (int)drPersonas["id_persona"];

                    p.Nombre = (string)((drPersonas["nombre"] != null) ? "" : drPersonas["nombre"]);
                    p.Apellido = (string)((drPersonas["apellido"] != null) ? "" : drPersonas["apellido"]);
                    p.Email = (string)((drPersonas["email"] != null) ? "" : drPersonas["email"]);
                    p.Direccion = (string)((drPersonas["direccion"] != null) ? "" : drPersonas["direccion"]);
                    p.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    p.IDPlan = (int?)((drPersonas["id_plan"] != null) ? null : drPersonas["id_plan"]);
                    p.Legajo = (int?)((drPersonas["legajo"] != null) ? null : drPersonas["legajo"]);
                    p.Telefono = (string)((drPersonas["telefono"] != null) ? "" : drPersonas["telefono"]);
                    p.Tipo = (Persona.TipoPersona)drPersonas["tipo_persona"];
                    p.Clave = (string)drPersonas["clave"];
                    p.Habilitado = (bool)drPersonas["habilitado"];
                    p.NombreUsuario = (string)drPersonas["nombre_usuario"];
                    //p.CambiaClave = (int)((drPersonas["cambia_clave"]!= null) ? null : drPersonas["cambia_clave"]);

                    personas.Add(p);
                }

                drPersonas.Close();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Personas de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return personas;
        }

        //trae una persona segun por el campo q se quiera buscar       
        public Persona GetOne(int ID = 0, int? legajo = 0, string nick ="")
        {
            string query = "";
            SqlParameter param = new SqlParameter();
            SqlCommand cmdPersonas;
            Persona p = new Persona();

            if (ID != 0) {
                query = @"SELECT*
                        FROM personas
                        WHERE id_persona = @id";

                param.SqlDbType = SqlDbType.Int;
                param.ParameterName = "@id";
                param.Value = ID;
            }
            if (legajo!= 0)
            {
                query = @"SELECT*
                        FROM personas
                        WHERE legajo = @legajo";

                param.SqlDbType = SqlDbType.Int;
                param.ParameterName = "@legajo";
                param.Value = legajo;
            }
            if (nick != "")
            {
                query = @"SELECT*
                        FROM personas
                        WHERE nombre_usuario = @nombre_usuario";

                param.SqlDbType = SqlDbType.VarChar;
                param.Size = 50;
                param.ParameterName = "@nombre_usuario";
                param.Value = nick;
            }
            
            try
            {
                this.OpenConnection();

                cmdPersonas = new SqlCommand(query, sqlConn);
                cmdPersonas.Parameters.Add(param);

                SqlDataReader drPersonas = cmdPersonas.ExecuteReader();
                if (drPersonas.Read())
                {
                    p.ID = (int)drPersonas["id_persona"];

                    p.Nombre = (string)((drPersonas["nombre"] != null) ? "" : drPersonas["nombre"]);
                    p.Apellido = (string)((drPersonas["apellido"] != null) ? "" : drPersonas["apellido"]);
                    p.Email = (string)((drPersonas["email"] != null) ? "" : drPersonas["email"]);
                    p.Direccion = (string)((drPersonas["direccion"] != null) ? "" : drPersonas["direccion"]);
                    p.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    p.IDPlan = (int?)((drPersonas["id_plan"] != null) ? null : drPersonas["id_plan"]);
                    p.Legajo = (int?)((drPersonas["legajo"] != null) ? null : drPersonas["legajo"]);
                    p.Telefono = (string)((drPersonas["telefono"] != null) ? "" : drPersonas["telefono"]);
                    p.Tipo = (Persona.TipoPersona)drPersonas["tipo_persona"];
                    p.Clave = (string)drPersonas["clave"];
                    p.Habilitado = (bool)drPersonas["habilitado"];
                    p.NombreUsuario = (string)drPersonas["nombre_usuario"];
                    //p.CambiaClave = (int)((drPersonas["cambia_clave"]!= null) ? null : drPersonas["cambia_clave"]);
                }
                drPersonas.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de la Persona de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            
            return p;
        }

        public void Save(Persona p)
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

        protected void Insert(Persona p)
        {
            
            try
            {
                
                this.OpenConnection();
                trans = sqlConn.BeginTransaction();

                SqlCommand cmdSave = new SqlCommand(@"INSERT INTO personas  
                                                        (nombre, apellido, direccion, email, telefono, fecha_nac, legajo, tipo_persona, id_plan, nombre_usuario, clave, habilitado)
                                                    VALUES
                                                        (@nombre, @apellido, @direccion, @email, @telefono, @fecha_nac, @legajo, @tipo_persona, @id_plan, @nombre_usuario, @clave, @habilitado)
                                                    SELECT @@indentity" //esta linea es para recuperar el ID que asigno el SQL automaticamente
                                                    , sqlConn);
                cmdSave.Transaction = trans;

                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = p.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = p.Apellido;
                cmdSave.Parameters.Add("@direccion", SqlDbType.VarChar, 50).Value = p.Direccion;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = p.Email;
                cmdSave.Parameters.Add("@telefono", SqlDbType.VarChar, 50).Value = p.Telefono;
                cmdSave.Parameters.Add("@fecha_nac", SqlDbType.DateTime ).Value = p.FechaNacimiento;
                cmdSave.Parameters.Add("@legajo", SqlDbType.Int).Value = p.Legajo;
                cmdSave.Parameters.Add("@tipo_persona", SqlDbType.Int).Value = (int)p.Tipo;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int ).Value = p.IDPlan;
                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = p.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = p.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = p.Habilitado;
                p.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); //asi se obtiene el ID que asigno al DB automaticamente


                trans.Commit();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                Exception ExcepcionManejada = new Exception("Error al insertar nuvea Persona en la DB ", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
                trans.Dispose();
            }
        }
        protected void Update(Persona p)
        {

            
            try
            {
                this.OpenConnection();
                trans = sqlConn.BeginTransaction();
                SqlCommand cmdSave = new SqlCommand(@"UPDATE personas SET 
                                                        nombre = @nombre, 
                                                        apellido = @apellido,
                                                        direccion = @direccion, 
                                                        email = @email, 
                                                        telefono = @telefono, 
                                                        fecha_nac = @fecha_nac,
                                                        legajo = @legajo, 
                                                        tipo_persona = @tipo_persona, 
                                                        id_plan = @id_plan,
                                                        nombre_usuario = @nombre_usuario,
                                                        clave = @clave,
                                                        habilitado = @habilitado
                                                    WHERE id_persona = @id"
                                                    , sqlConn);
                cmdSave.Transaction = trans;

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = p.ID;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = p.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = p.Apellido;
                cmdSave.Parameters.Add("@direccion", SqlDbType.VarChar, 50).Value = p.Direccion;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = p.Email;
                cmdSave.Parameters.Add("@telefono", SqlDbType.VarChar, 50).Value = p.Telefono;
                cmdSave.Parameters.Add("@fecha_nac", SqlDbType.DateTime).Value = p.FechaNacimiento;
                if (p.Legajo == null)
                {
                    cmdSave.Parameters.Add("@legajo", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    cmdSave.Parameters.Add("@legajo", SqlDbType.Int).Value = p.Legajo;
                }
                //cmdSave.Parameters.Add("@legajo", SqlDbType.Int).Value = p.Legajo == null ? DBNull.Value : p.Legajo;

                cmdSave.Parameters.Add("@tipo_persona", SqlDbType.Int ).Value = (int)p.Tipo;
                if (p.IDPlan == null)
                {
                    cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = p.IDPlan;
                }
                //cmdSave.Parameters.Add("@id_plan", SqlDbType.Int ).Value = p.IDPlan;

                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = p.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = p.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = p.Habilitado;
                cmdSave.ExecuteNonQuery();

              
                trans.Commit();
            }
            catch(Exception ex)
            {
                trans.Rollback();
                Exception ExcepcionManejada = new Exception("Error al modificar la Persona en la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
                trans.Dispose();
            }

        }
        public void Delete(int ID)
        {
            
            try
            {
                this.OpenConnection();
                trans = sqlConn.BeginTransaction();

                SqlCommand cmdDelete = new SqlCommand("DELETE FROM personas WHERE id_persona = @id", sqlConn);
                cmdDelete.Transaction = trans;
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
                            

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Exception ExcepcionManejada = new Exception("Error al eliminar la persona y sus usuarios de la DB", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
                trans.Dispose();
            }


        }

        
    }
}
