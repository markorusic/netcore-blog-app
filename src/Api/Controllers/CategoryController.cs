using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dao.Utils;
using Domain;
using Dto.Request;
using Dto.Response;
using Dto.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public Page<CategoryResponseDto> Get([FromQuery] CategorySearchDto request)
        {
            return _categoryService.FindAll(request);
        }

        [HttpGet("{id}")]
        public CategoryResponseDto GetOne(int id)
        {
            return _categoryService.FindById(id);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public CategoryResponseDto Post([FromBody] CategoryRequestDto request)
        {
            return _categoryService.Create(request);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public CategoryResponseDto Put(int id, [FromBody] CategoryRequestDto request)
        {
            return _categoryService.Update(id, request);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _categoryService.Delete(id);
        }
    }
}
