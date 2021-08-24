using Models.Comparisons;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SDK.Resources
{
    public interface ICompareResource
    {


        /// <summary>
        /// gets all entities currently present in the database
        /// </summary>
        [Get("/v1/diff")]
        Task<List<ComparisonModel>> GetAll();


        /// <summary>
        /// compares sides and returns result
        /// </summary>
        [Get("/v1/diff/{ID}")]
        Task<ComparisonModel> Result(Guid ID);


        /// <summary>
        /// adds rights side
        /// </summary>
        [Post("/v1/diff/{ID}/right")]
        Task AddRight(Guid ID,byte[] right);

        /// <summary>
        /// adds rights side
        /// </summary>
        [Post("/v1/diff/{ID}/left")]
        Task AddLeft(Guid ID, byte[] left);
    }
}
