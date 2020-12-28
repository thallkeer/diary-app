﻿using AutoMapper;
using DiaryApp.API.Exceptions;
using DiaryApp.API.Models;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Interfaces;
using DiaryApp.Data.Exceptions;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers
{
    public class PageController<TPageDto> : AppBaseController<PageController<TPageDto>>
        where TPageDto : PageDto
    {
        protected readonly IPageService<TPageDto> pageService;

        public PageController(IPageService<TPageDto> pageService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
        {
            this.pageService = pageService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPageAsync([FromQuery] PageRequest pageRequest)
        {
            var page = await pageService.GetPageAsync(pageRequest.UserId, pageRequest.Year, pageRequest.Month);

            if (page == null)
                return await PostPageAsync(pageRequest);

            return Ok(page);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostPageAsync(PageRequest pageParams)
        {
            TPageDto page;
            try
            {
                page = await pageService.CreateAsync(pageParams.UserId, pageParams.Year, pageParams.Month);
            }
            catch (PageAlreadyExistsException e)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, e.Message);
            }
            catch (UserNotExistsException ex)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            return Ok(page);
        }

        protected async Task<IActionResult> GetPageArea<TPageArea, TPageAreaDto>(int pageID)
            where TPageArea : class, IPageArea
            where TPageAreaDto : PageAreaDto
        {
            TPageAreaDto area = await pageService.GetPageArea<TPageAreaDto, TPageArea>(pageID);
            if (area == null)
            {
                string err = $"Page area {typeof(TPageArea)} not found for pageID {pageID}";
                logger.LogError(err);
                return NotFound(err);
            }
            return Ok();
        }
    }
}