using API.SDK.Resources;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SDK
{
    public class ApiClient
    {
        private readonly ServiceHttpClient httpClient;
        public ICompareResource Comparisons { get; set; }


        public ApiClient(ServiceHttpClient httpClient)
        {
            Comparisons = RestService.For<ICompareResource>(httpClient);

            this.httpClient = httpClient;
        }
    }
}
