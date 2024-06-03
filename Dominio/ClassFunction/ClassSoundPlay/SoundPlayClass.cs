using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ClassSoundPlay
{
    public class SoundPlayClass
    {
        public void SoundPlay(string NameSound)
        {
            // Nombre del archivo de sonido
            string nombreArchivoSonido = NameSound + ".wav"; // Reemplaza con el nombre de tu archivo de sonido

            // Obtener la ruta del directorio donde se encuentra el ejecutable
            string directorioEjecutable = AppDomain.CurrentDomain.BaseDirectory;

            // Construir la ruta completa al archivo de sonido
            string rutaArchivoSonido = Path.Combine(directorioEjecutable, nombreArchivoSonido);

            // Crear una instancia de SoundPlayer
            SoundPlayer reproductor = new SoundPlayer(rutaArchivoSonido);

            try
            {
                // Reproducir el sonido
                reproductor.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al reproducir el sonido: {ex.Message}");
            }
            finally
            {
                // Liberar recursos del SoundPlayer
                reproductor.Dispose();
            }
        }
    }
}
