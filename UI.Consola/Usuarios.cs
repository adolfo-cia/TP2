using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Logic;
namespace UI.Consola
{
    public class Usuarios
    {
        private Business.Logic.UsuarioLogic _UsuarioNegocio;
        public Business.Logic.UsuarioLogic UsuarioNegocio
        {
            get { return _UsuarioNegocio; }
            set { _UsuarioNegocio = value; }

        }

        public Usuarios ()
        {
            UsuarioNegocio = new UsuarioLogic();
        }


        public void menu()
        {

            bool repetir = true;
            do
            {
                int op;
                System.Console.WriteLine(
                    "Elija una opcion: \n 1- Listado General" +
                    "\n 2- Consulta \n 3-Agregar \n 4-Modificar \n 5-Eliminar \n 6-Salir");
                op = Int32.Parse(System.Console.ReadLine());
                switch (op)
                {
                    case 1:
                        this.ListadoGeneral();
                        break;

                    case 2:
                        this.Consultar();
                        break;

                    case 3:
                        this.Agregar();
                        break;

                    case 4:
                        this.Modificar();
                        break;

                    case 5:
                        this.Eliminar();
                        break;

                    case 6:
                        repetir = false;
                        break;

                    default:
                        System.Console.WriteLine("sorete ingresa bien");
                        break;
                }

            } while (repetir);




        }

        public void Consultar()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Ingrese el ID del usuario a consultar ");
                int ID = int.Parse(Console.ReadLine());
                this.MostrarDatos(UsuarioNegocio.GetOne(ID));

            }
            catch (FormatException fe)
            {
                Console.WriteLine();
                Console.WriteLine("La ID ingresada debe ser un número entero");    
            }
            catch(Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Presione una tecla para continuar");
                Console.ReadKey();
            }

        }      
        public void ListadoGeneral()
        {
            Console.Clear();
            foreach (Usuario usr in UsuarioNegocio.GetAll())
            {
                MostrarDatos(usr);
            }
        }
        
        public void MostrarDatos(Usuario usr)
        {
            Console.WriteLine("Usuario: " + usr.ID);
            Console.WriteLine("\t\tNombre " + usr.Nombre);
            Console.WriteLine("\t\tApellido " + usr.Apellido);
            Console.WriteLine("\t\tNombre de Usuario " + usr.NombreUsuario);
            Console.WriteLine("\t\tClave " + usr.Clave);
            Console.WriteLine("\t\tEmail " + usr.Email);
            Console.WriteLine("\t\tHabilitado " + usr.Habilitado);
            Console.WriteLine();
        }

        public void Modificar()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Ingrese el ID del usuario a modificar");
                int ID = int.Parse(Console.ReadLine());
                Usuario usuario = UsuarioNegocio.GetOne(ID);
                Console.WriteLine("Ingrese Nombre: ");
                usuario.Nombre = Console.ReadLine();
                Console.WriteLine("Ingrese Apellido :");
                usuario.Apellido = Console.ReadLine();
                Console.WriteLine("Ingrese Nombre de Usuario :");
                usuario.NombreUsuario = Console.ReadLine();
                Console.WriteLine("Ingrese Clave :");
                usuario.Clave = Console.ReadLine();
                Console.WriteLine("Ingrese Email :");
                usuario.Email = Console.ReadLine();
                Console.WriteLine("Ingrese Habilitación de Usuario (1-Si/otro-No): ");
                usuario.Apellido = Console.ReadLine();
                usuario.Habilitado = (Console.ReadLine() == "1");
                usuario.State = BusinessEntity.States.Modified;
                UsuarioNegocio.Save(usuario);
        
            }
            catch (FormatException fe)
            {
                Console.WriteLine();
                Console.WriteLine("La ID ingresada debe ser un número entero");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Presione una tecla para continuar");
                Console.ReadKey();
            }
        }

        public void Agregar ()
        {
            Usuario usuario = new Usuario();
            Console.Clear();
            Console.WriteLine("Ingrese Nombre: ");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese Apellido :");
            usuario.Apellido = Console.ReadLine();
            Console.WriteLine("Ingrese Nombre de Usuario :");
            usuario.NombreUsuario = Console.ReadLine();
            Console.WriteLine("Ingrese Clave :");
            usuario.Clave = Console.ReadLine();
            Console.WriteLine("Ingrese Email :");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese Habilitación de Usuario (1-Si/otro-No): ");
            usuario.Apellido = Console.ReadLine();
            usuario.Habilitado = (Console.ReadLine() == "1");
            usuario.State = BusinessEntity.States.New;
            UsuarioNegocio.Save(usuario);
            Console.WriteLine();
            Console.WriteLine("ID " + usuario.ID);  
        }

        public void Eliminar()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Ingrese el ID del usuario a eliminar");
                int ID = int.Parse(Console.ReadLine());
                UsuarioNegocio.Delete(ID);
            }

            catch (FormatException fe)
            {
                Console.WriteLine();
                Console.WriteLine("La ID ingresada debe ser un número entero");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Presione una tecla para continuar");
                Console.ReadKey();
            }
        }
        

    }
}
