using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class UsuarioAdapter:Adapter
    {
        #region DatosEnMemoria
        // Esta región solo se usa en esta etapa donde los datos se mantienen en memoria.
        // Al modificar este proyecto para que acceda a la base de datos esta será eliminada
        //private static List<Usuario> _Usuarios;
        //private static List<Usuario> Usuarios
        //{
        //    get
        //    {
        //        if (_Usuarios == null)
        //        {
        //            _Usuarios = new List<Business.Entities.Usuario>();
        //            Business.Entities.Usuario usr;
        //            usr = new Business.Entities.Usuario();
        //            usr.ID = 1;
        //            usr.State = Business.Entities.BusinessEntity.States.Unmodified;
        //            usr.Nombre = "Casimiro";
        //            usr.Apellido = "Cegado";
        //            usr.NombreUsuario = "casicegado";
        //            usr.Clave = "miro";
        //            usr.Email = "casimirocegado@gmail.com";
        //            usr.Habilitado = true;
        //            _Usuarios.Add(usr);

        //            usr = new Business.Entities.Usuario();
        //            usr.ID = 2;
        //            usr.State = Business.Entities.BusinessEntity.States.Unmodified;
        //            usr.Nombre = "Armando Esteban";
        //            usr.Apellido = "Quito";
        //            usr.NombreUsuario = "aequito";
        //            usr.Clave = "carpintero";
        //            usr.Email = "armandoquito@gmail.com";
        //            usr.Habilitado = true;
        //            _Usuarios.Add(usr);

        //            usr = new Business.Entities.Usuario();
        //            usr.ID = 3;
        //            usr.State = Business.Entities.BusinessEntity.States.Unmodified;
        //            usr.Nombre = "Alan";
        //            usr.Apellido = "Brado";
        //            usr.NombreUsuario = "alanbrado";
        //            usr.Clave = "abrete sesamo";
        //            usr.Email = "alanbrado@gmail.com";
        //            usr.Habilitado = true;
        //            _Usuarios.Add(usr);

        //        }
        //        return _Usuarios;
        //    }
        //}
        #endregion


        public List<Usuario> GetAll()
        {
            //instanciamos el objeto lista a retornar
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                //abrimos la conexion a la DB con el metodo del Adapter, el cual hereda UsuarioAdapter, osea this.
                this.OpenConnection();

                //creamos un objeto SqlCommand q sera la sentencia SQL q vamos a ejecutar contra la DB
                //sqlConn, en este caso, guarda la info declarada en el metodo Adapter, q contiene los datos para realizar la conexion contra la DB
                SqlCommand cmdUsuarios = new SqlCommand("select * from usuarios", sqlConn);

                //instanciamos un objeto DataReader q sera el q recuperara los datos de la DB
                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();

                //Read() lee una fila devuelta por el command SQL
                //carga los datos en drUsuarios para poder accederlos,
                //devuelve true mientras haya podido leer los datos
                //y avanza a la siguiente fila para el proximo read
                while (drUsuarios.Read())
                {
                    //creamos una entidad Usuario para copiar los datos de la fila del DataReader a esta entidad
                    Usuario usr = new Usuario();
                    //ahora copiamos los datos de la fila a la entidad
                    usr.ID = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.Email = (string)drUsuarios["email"];
                    //agregamos la entidad a la lista de entidades q devolveremos
                    usuarios.Add(usr);
                }

                //cerramos todo
                drUsuarios.Close();
                
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Usuarios", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            //devolvemos la lista de usuarios
            return usuarios;
        }
        public Usuario GetOne(int ID)
        {
            Usuario usr = new Usuario();
            try
            {
                this.OpenConnection();
                SqlCommand cmdUsuarios = new SqlCommand("select * from usuarios where id_usuario = @id", sqlConn);
                cmdUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();
                if (drUsuarios.Read())
                {
                    usr.ID = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.Email = (string)drUsuarios["email"];
                }
                drUsuarios.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos del usuario", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            
            return usr;
        }
        public void Save(Usuario usuario)
        {
            if (usuario.State == BusinessEntity.States.Deleted)
            {
                this.Delete(usuario.ID);
            }
            else if (usuario.State == BusinessEntity.States.New)
            {
                this.Insert(usuario);
            }
            else if (usuario.State == BusinessEntity.States.Modified)
            {
                this.Update(usuario);
            }
            usuario.State = BusinessEntity.States.Unmodified;
        }

        protected void Insert(Usuario usuario)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"INSERT INTO usuarios  
                                                        (nombre_usuario, clave, habilitado, nombre, apellido, email)
                                                    VALUES
                                                        (@nombre_usuario, @clave, @habilitado, @nombre, @apellido, @email)
                                                    SELECT @@indentity" //esta linea es para recuperar el ID que asigno el SQL automaticamente
                                                    , sqlConn);

                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = usuario.Apellido;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = usuario.Email;
                usuario.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); //asi se obtiene el ID que asigno al DB automaticamente

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear usuario", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(Usuario usuario)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(@"UPDATE usuarios SET 
                                                        nombre_usuario = @nombre_usuario, 
                                                        clave = @clave,
                                                        habilitado = @habilitado, 
                                                        nombre = @nombre, 
                                                        apellido = @apellido, 
                                                        email = @email
                                                    WHERE id_usuario = @id"
                                                    , sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = usuario.ID;
                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = usuario.Apellido;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = usuario.Email;
                cmdSave.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar el usuario", ex);
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
                //abro la conexion
                this.OpenConnection();
                //creamos la sentencia sql y asignamos un valor al parametro
                SqlCommand cmdDelete = new SqlCommand("DELETE FROM usuarios WHERE id_usuario = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                //ejecutamos la sentencia sql
                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar el usuario", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }


        }

        
    }
}
