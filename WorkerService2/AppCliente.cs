using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService2
{
    public class AppCliente
    {
        public Queue<string> Input { get; set; }
        public Queue<string> Output { get; set; }

        protected virtual bool active { get; set; }
        
        public AppCliente()
        {
            Input = new Queue<string>();
            Output = new Queue<string>();
            active = false;
        }

        public async Task run()
        {
            if (!active)
            {
                active = true;
            }else
            {
                return;
            }
            await Task.Run(()=> {
                while (active)
                {
                    if(this.Input.Count > 0)
                    {
                        var message = this.Input.Dequeue();
                        if (message.ToUpper().IndexOf("STOP") > -1)
                        {
                            active = false;
                            Output.Enqueue("Se ha detenido el proceso.");
                            continue;
                        }
                        var process = $"mensaje: {message} procesado";
                        Output.Enqueue(process);
                    }
                    else
                    {
                        active = false;
                        Output.Enqueue("Se ha detenido el proceso no existen registros.");
                    }
                   
                }
                
            });
        }

        public void stop()
        {
            active = false;
        }


    }
}
