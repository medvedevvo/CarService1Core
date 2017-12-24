using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using CarService1.Models;

namespace CarService1
{
    public class Startup
    {
        private Timer sys_timer;
        private Random rng = new Random();
        private DBObjects dbObj = DBObjects.getInstance();
        private int cnt = 0;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var autoEvent = new AutoResetEvent(false);
            sys_timer = new Timer(sys_timer_Tick, autoEvent, 0, 1000);
        }

        //--- Изменеие состояния аккумуляторов по таймеру -------------------------------------------------------------
        private void sys_timer_Tick(object sender)
        {
            dbObj.objects_list[1].parameters[1].val = cnt.ToString();

            modify_accu(2, cnt);
            modify_accu(3, cnt*2);

            cnt++;
            if (cnt < 0) cnt = 0;
            if (cnt > 1000) cnt = 0;
        }
        private void modify_accu(int id, int cnt_charge)
        {
            double rand = (rng.NextDouble() - 0.5);
            double val = 12.3 + rand;
            string s = (val.ToString()).Replace(',', '.');
            s = s.Remove(s.IndexOf('.') + 4);
            dbObj.objects_list[id].parameters[0].val = s;

            rand = (rng.NextDouble() - 0.5);
            val = 5.0 + rand;
            s = (val.ToString()).Replace(',', '.');
            s = s.Remove(s.IndexOf('.') + 4);
            dbObj.objects_list[id].parameters[1].val = s;

            s = dbObj.objects_list[2].parameters[2].val;
            double d = cnt_charge / 10.0;
            if (d > 99.0) d = 99.0;
            int charge = 100 - Convert.ToInt32(d);
            dbObj.objects_list[id].parameters[2].val = charge.ToString();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
