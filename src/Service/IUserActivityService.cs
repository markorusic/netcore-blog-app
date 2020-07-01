using Dao.Utils;
using Domain;
using Dto.Response;
using Dto.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IUserActivityService
    {
        public void Track(string actionType);

        public Page<UserActivityResponseDto> FindAll(UserActivitySearchDto request);
    }
}
