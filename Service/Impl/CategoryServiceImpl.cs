using AutoMapper;
using Dao;
using Dao.Utils;
using Domain;
using Dto.Request;
using Dto.Response;
using Dto.Search;
using ResourceException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Impl
{
    public class CategoryServiceImpl : ICategoryService
    {
        private readonly IMapper _mapper;

        private readonly AppDb _db;

        public CategoryServiceImpl(IMapper mapper, AppDb db)
        {
            _mapper = mapper;
            _db = db;
        }

        public CategoryResponseDto Create(CategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description
            };

            _db.Categories.Add(category);
            _db.SaveChanges();

            return _mapper.Map<CategoryResponseDto>(category);
        }

        public void Delete(int categoryId)
        {
            var category = _db.Categories.FirstOrDefault(category => category.Id == categoryId);
            if (category == null)
            {
                throw new ResourceNotFoundException("Category");
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }

        public Page<CategoryResponseDto> FindAll(CategorySearchDto request)
        {
            var query = _db.Categories.AsQueryable();

            if (request.Name != null)
            {
                query = query.Where(category => category.Name.ToLower().Contains(request.Name.ToLower()));
            }

            return query
                .Select(category => _mapper.Map<CategoryResponseDto>(category))
                .GetPaged(request.Page, request.Size);
        }

        public CategoryResponseDto FindById(int categoryId)
        {
            var category = _db.Posts.FirstOrDefault(category => category.Id == categoryId);
            if (category == null)
            {
                throw new ResourceNotFoundException("Category");
            }
            return _mapper.Map<CategoryResponseDto>(category);
        }

        public CategoryResponseDto Update(int categoryId, CategoryRequestDto request)
        {
            var category = _db.Categories.FirstOrDefault(category => category.Id == categoryId);
            if (category == null)
            {
                throw new ResourceNotFoundException("Category");
            }

            category.Name = request.Name;
            category.Description = request.Description;

            _db.SaveChanges();

            return _mapper.Map<CategoryResponseDto>(category);
        }
    }
}
