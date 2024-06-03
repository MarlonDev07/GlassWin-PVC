using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuscadorActualizaciones
{
    public partial class frmActualizaciones : Form
    {
        public frmActualizaciones()
        {
            InitializeComponent();
            VerificarActualizaciones();
        }

        // URL del servidor FTP y nombre de usuario/contraseña
        private string ftpUrl = "ftp://138.59.135.48/";
        private string ftpUsername = "ftpUser";
        private string ftpPassword = "GlassWinFTP0192";

        public void VerificarActualizaciones()
        {
            try
            {
                // Obtener la versión actual de la aplicación (puedes implementar tu propia lógica aquí)
                Version versionActual = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                // Construir la URL del archivo de versión en el servidor FTP
                string versionUrl = ftpUrl + "version.txt";

                // Descargar el archivo de versión
                string versionString;
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    versionString = client.DownloadString(versionUrl);
                }

                // Convertir el string de versión en un objeto Version
                Version versionNueva = new Version(versionString);

                // Comparar versiones
                if (versionNueva > versionActual)
                {
                    // Si hay una nueva versión disponible, descargar archivos de actualización
                    DescargarArchivosActualizacion();
                }
                else
                {
                    // No hay actualizaciones disponibles
                    MessageBox.Show("Tu aplicación está actualizada.", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar actualizaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Restaurar la Carpeta GlassWin que se borro
            }
        }

        // Método para descargar archivos de actualización desde el servidor FTP
        private void DescargarArchivosActualizacion()
        {
            //Pedir Permisos de Administrador
            if (!ElevateToAdmin())
            {
                MessageBox.Show("No se pudo obtener permisos de administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Eliminar la Carpeta GlassWin que Esta en Documentos
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GlassWin";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }


            // Ejemplo: Descargar un archivo llamado "MiApp.exe"
            string archivoUrl = ftpUrl + "GlassWin";

            //Obtener un Direcctorio Padre
            string CarDocument = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);


            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                client.DownloadFile(archivoUrl, CarDocument);
            }

            MessageBox.Show("Actualización completada", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Ejecutar el archivo de actualización
            System.Diagnostics.Process.Start(CarDocument + "\\GlassWin\\GlassWin.exe");

            //Cerrar la aplicación actual
            Application.Exit();
        }

        private bool ElevateToAdmin()
        {
            // Verificar si el usuario actual es administrador
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            bool isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

            // Si el usuario no es administrador, volver a ejecutar la aplicación con permisos elevados
            if (!isAdmin)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Application.ExecutablePath;
                startInfo.Verb = "runas";

                try
                {
                    Process.Start(startInfo);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }
    }
}
