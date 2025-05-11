using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Tracing.Text
{
    public class LogText : ITraceText
    {
        private string _ruta;

        private IOptions<LogSettings> _logSettings;
        public LogText(IOptions<LogSettings> logSettings)
        {
            _logSettings = logSettings;
            _ruta = string.Format("{0}{1}", _logSettings.Value.Path, string.Format("{0:yyyyMMdd}{1}", DateTime.Now, _logSettings.Value.FileName));
        }

        public void RegistrarEvento(string message)
        {
            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            this.RegistrarConWriterText(message);
            //}).Start();
        }

        public void RegistrarEvento(List<string> values)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                this.RegistrarConWriterText(values);
            }).Start();
        }

        public void RegistrarEvento(ErrorMessage errorTemplate, params string[] values)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                this.RegistrarConWriterText(errorTemplate, values);
            }).Start();
        }

        public void RegistrarEvento(ServiceMessage serviceMessage)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                this.RegistrarConWriterText(serviceMessage);
            }).Start();
        }

        public void RegistrarEvento(Exception ex, params string[] values)
        {
            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            this.RegistrarConWriterText(ex, values);
            //}).Start();
        }

        private void RegistrarConWriterText(string mensaje)
        {
            mensaje = $"{DateTime.Now} {mensaje}";

            (new WriterText()).RegistrarEvento(_ruta, mensaje);
        }

        private void RegistrarConWriterText(List<string> valueList)
        {
            string mensaje = "";

            mensaje = $"{DateTime.Now}";
            valueList.ForEach(x =>
            {
                mensaje += $"{x}";
            });

            (new WriterText()).RegistrarEvento(_ruta, mensaje);
        }

        private void RegistrarConWriterText(ServiceMessage serviceMessage)
        {
            string mensaje = Newtonsoft.Json.JsonConvert.SerializeObject(serviceMessage); ;

            (new WriterText()).RegistrarEvento(_ruta, $"{mensaje}");
        }

        private void RegistrarConWriterText(ErrorMessage errorTemplate, params string[] values)
        {
            string mensaje = "";

            string valueString = "";
            Array.ForEach(values, x =>
            {
                valueString += string.Format("{0};", (!string.IsNullOrEmpty(x) ? x : string.Empty));
            });

            mensaje = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}"
                                    , DateTime.Now
                                    , Environment.NewLine
                                    , errorTemplate.Servidor
                                    , Environment.NewLine
                                    , errorTemplate.Usuario
                                    , Environment.NewLine
                                    , errorTemplate.Mensaje
                                    , Environment.NewLine
                                    , errorTemplate.Inner
                                    , Environment.NewLine
                                    , errorTemplate.Stack
                                    , Environment.NewLine
                                    , valueString
                                    , Environment.NewLine);

            (new WriterText()).RegistrarEvento(_ruta, string.Format("{0}{1}", mensaje, Environment.NewLine));
        }

        private void RegistrarConWriterText(Exception ex, params string[] values)
        {
            string mensaje = "";

            Array.ForEach(values, x =>
            {
                mensaje += string.Format("{0}{1}", (!string.IsNullOrEmpty(x) ? x : string.Empty), Environment.NewLine);
            });

            mensaje = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}"
                                    , DateTime.Now
                                    , Environment.NewLine
                                    , mensaje
                                    , ex.Message
                                    , Environment.NewLine
                                    , ex.InnerException
                                    , Environment.NewLine
                                    , ex.StackTrace
                                    , Environment.NewLine);

            (new WriterText()).RegistrarEvento(_ruta, string.Format("{0}{1}", mensaje, Environment.NewLine));
        }

        private void RegistrarConWriterText(Exception ex)
        {
            string mensaje = "";

            mensaje = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}"
                                    , DateTime.Now
                                    , Environment.NewLine
                                    , ex.Message
                                    , Environment.NewLine
                                    , ex.InnerException
                                    , Environment.NewLine
                                    , ex.StackTrace
                                    , Environment.NewLine);

            (new WriterText()).RegistrarEvento(_ruta, string.Format("{0}{1}", mensaje, Environment.NewLine));
        }

        private void RegistrarConWriterText(params string[] values)
        {
            string mensaje = "";

            if (!values.Any())
                return;

            Array.ForEach(values, x =>
            {
                mensaje += string.Format("{0}{1}", (!string.IsNullOrEmpty(x) ? x : string.Empty), Environment.NewLine);
            });

            mensaje = string.Format("{0}{1}{2}{3}"
                                    , DateTime.Now
                                    , Environment.NewLine
                                    , mensaje
                                    , Environment.NewLine);

            (new WriterText()).RegistrarEvento(_ruta, string.Format("{0}{1}", mensaje, Environment.NewLine));
        }
    }
}
