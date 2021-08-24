using Core.Entities;
using Core.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Comparisons;
using Models.Responses;
using Newtonsoft.Json;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("v1/diff")]
    [ApiController]
    public class ComparisonController : ControllerBase
    {
        private readonly DefaultDbContext _dbContext;
        private readonly IComparisonService _comparisonService;
        public ComparisonController(DefaultDbContext dbContext, IComparisonService comparisonService)
        {
            _dbContext = dbContext;
            _comparisonService = comparisonService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comparisons = await _comparisonService.GetAll();
            return Ok(comparisons);
        }

        [HttpPost("{ID}/right")]
        public async Task<IActionResult> Right(Guid ID, byte[] base64EncodedJSON)
        {
            string decodedJSON = Encoding.UTF8.GetString(base64EncodedJSON);

            var rightSide = JsonConvert.DeserializeObject<SideModel>(decodedJSON);

            if(rightSide != null && !string.IsNullOrEmpty(rightSide.input))
            {

                try
                {
                    var comparison = await _comparisonService.Get().FirstOrDefaultAsync(x => x.Id == ID);

                    if (comparison != null)
                    {
                        comparison.RightSide = rightSide.input;

                        var response = await _comparisonService.UpdateAsync(comparison);

                        if (response.HasSucceeded)
                        {
                            return Ok();
                        }

                        return BadRequest(response.Message);
                    }
                    else
                    {
                        comparison = new ComparisonEntity();
                        comparison.RightSide = rightSide.input;
                        comparison.Id = ID; //I know this shoud not be assigned, but since new guid is generated in web application I think it's fine if I assign it(because it is in the route)


                        var response = await _comparisonService.AddAsync(comparison);

                        if (response.HasSucceeded)
                        {
                            return Ok();
                        }

                        return BadRequest(response.Message);
                    }

                }
                catch
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest("Input is empty!");
            }

        }

        [HttpPost("{ID}/left")]
        public async Task<IActionResult> Left(Guid ID, byte[] base64EncodedJSON)
        {
            string decodedJSON = Encoding.UTF8.GetString(base64EncodedJSON);

            
            var leftSide = JsonConvert.DeserializeObject<SideModel>(decodedJSON);

            if (leftSide != null && !string.IsNullOrEmpty(leftSide.input))
            {

                try
                {
                    var comparison = await _comparisonService.Get().FirstOrDefaultAsync(x => x.Id == ID);

                    if (comparison != null)
                    {
                        comparison.LeftSide = leftSide.input;

                        var response = await _comparisonService.UpdateAsync(comparison);

                        if (response.HasSucceeded)
                        {
                            return Ok();
                        }

                        return BadRequest(response.Message);
                    }
                    else
                    {
                        comparison = new ComparisonEntity();
                        comparison.LeftSide = leftSide.input;
                        comparison.Id = ID; //I know this shoud not be assigned, but since new guid is generated in web application I think it's fine if I assign it(because it is in the route)


                        var response = await _comparisonService.AddAsync(comparison);

                        if (response.HasSucceeded)
                        {
                            return Ok();
                        }

                        return BadRequest(response.Message);
                    }

                }
                catch
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest("Input is empty!");
            }
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> Result(Guid ID)
        {
            try
            {
                var comparison = await _comparisonService.GetByIdAsync(ID);

                if(comparison == null)
                {
                    return BadRequest(new ComparisonResponse()
                    {
                        Message = "no inputs were found to be diffed"
                    });
                }

                if(string.IsNullOrEmpty(comparison.RightSide))
                {
                    return BadRequest(new ComparisonResponse()
                    {
                        Message = "right side input is missing"
                    });
                }

                if (string.IsNullOrEmpty(comparison.LeftSide))
                {
                    return BadRequest(new ComparisonResponse()
                    {
                        Message = "left side input is missing"
                    });
                }


                var result = _comparisonService.Compare(comparison.LeftSide, comparison.RightSide);


                return Ok(new ComparisonResponse()
                {
                    Message = !string.IsNullOrEmpty(result.Result) ? result.Result : null,
                    Offsets = result.Offsets != null && result.Offsets.Count > 0 ? result.Offsets : null,
                    IsSucceeded = !string.IsNullOrEmpty(result.Result) || (result.Offsets != null && result.Offsets.Count > 0 )
                });

            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
