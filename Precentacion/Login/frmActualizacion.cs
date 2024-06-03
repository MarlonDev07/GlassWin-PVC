using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Precentacion.Login
{
    public partial class frmActualizacion : Form
    {
        // URL del servidor FTP y nombre de usuario/contraseña
        private string ftpUrl = "ftp://138.59.135.48/";
        private string ftpUsername = "ftpUser";
        private string ftpPassword = "GlassWinFTP0192";
        // Versiones
        Version versionNueva;
        Version versionActual;

        public frmActualizacion()
        {
            InitializeComponent();
           
        }

        private async void frmActualizacion_Load(object sender, EventArgs e)
        {
            // Mostrar el formulario
            this.Show();

            // Iniciar la barra de progreso
            progressBar1.Style = ProgressBarStyle.Marquee;
            timer1.Start();

            // Ejecutar la tarea de actualización en segundo plano
            await Actualizacion();
        }

        private async Task Actualizacion()
        {
            // Ejecutar VerificarActualizaciones en un subproceso separado
            bool actualizacionesDisponibles = await Task.Run(() => VerificarActualizaciones());

            // Descargar archivos de actualización si hay actualizaciones disponibles
            if (actualizacionesDisponibles)
            {
                bool descargaExitosa = await DescargarArchivosActualizacion();

                if (descargaExitosa)
                {
                    // Actualización exitosa
                    Application.Exit();
                }
                else
                {
                    frmLogin login = new frmLogin();
                    login.Show();
                    this.Close();
                }
            }
            else
            {
                // No hay actualizaciones disponibles, mostrar formulario de inicio de sesión
                frmLogin login = new frmLogin();
                login.Show();
                this.Hide();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Actualizar la barra de progreso infinitamente
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 1;
                progressBar1.Value -= 1;
            }
        }

        public bool VerificarActualizaciones()
        {

            try
            {
                // Obtener la versión actual de la aplicación (puedes implementar tu propia lógica aquí)
                versionActual = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

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
                versionNueva = new Version(versionString);

                // Comparar versiones
                if (versionNueva > versionActual)
                {
                    return true;
                }
                else
                {
                    // No hay actualizaciones disponibles
                    MessageBox.Show("Tu aplicación está actualizada.", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar actualizaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Método para descargar archivos de actualización desde el servidor FTP
        public async Task<bool> DescargarArchivosActualizacion()
        {
            // URL del archivo ZIP en el servidor FTP
            string archivoZipUrl = ftpUrl + "GlassWin" + versionNueva.ToString() + ".zip";

            // Directorio de Documentos
            string parentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            try
            {
                // Ruta del archivo ZIP de destino
                string archivoZipDestino = Path.Combine(parentDirectory, "GlassWin" + versionNueva.ToString() + ".zip");

                // Descargar el archivo ZIP desde el servidor FTP
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    client.DownloadFile(archivoZipUrl, archivoZipDestino);
                }

                // Descomprimir el archivo ZIP en el directorio de documentos
                using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(archivoZipDestino)))
                {
                    ZipEntry entry;
                    while ((entry = zipStream.GetNextEntry()) != null)
                    {
                        string entryPath = Path.Combine(parentDirectory, entry.Name);
                        string entryDir = Path.GetDirectoryName(entryPath);

                        if (!Directory.Exists(entryDir))
                        {
                            Directory.CreateDirectory(entryDir);
                        }

                        if (!entry.IsDirectory)
                        {
                            using (FileStream outputStream = File.Create(entryPath))
                            {
                                byte[] buffer = new byte[4096];
                                int bytesRead;
                                while ((bytesRead = zipStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    outputStream.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }
                }

                //Eliminar la carpeta anterior
                try
                {
                    string oldFolder = Path.Combine(parentDirectory, "GlassWin" + versionActual);
                    if (Directory.Exists(oldFolder))
                    {
                        Directory.Delete(oldFolder, true);
                    }
                }
                catch (Exception)
                {
                }

                // Eliminar el archivo ZIP después de descomprimirlo
                File.Delete(archivoZipDestino);

                //Crear un Acceso directo en el escritorio de la aplicación GlassWin.exe
                string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "GlassWin.lnk");
                string targetPath = Path.Combine(parentDirectory + "\\GlassWin" + versionNueva, "GlassWin.exe");
                CrearAccesoDirecto(shortcutPath, targetPath);

                MessageBox.Show("Actualización completada. Por favor reinicia la aplicación.", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Salir de la aplicación
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Acceso denegado a la ruta de acceso: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar archivos de actualización: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Método para crear un acceso directo en el escritorio
        private void CrearAccesoDirecto(string shortcutPath, string targetPath)
        {
            // Crear un objeto WshShell
            dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));

            // Crear el acceso directo
            dynamic shortcut = shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = targetPath;
            shortcut.Save();
        }
    }
}
