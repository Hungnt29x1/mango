﻿using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;

        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get() {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                //return objList;
                _responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon objList = _db.Coupons.First(u => u.CouponId == id);
                //CouponDto dto = new CouponDto()
                //{
                //    CouponId = objList.CouponId,
                //    CouponCode = objList.CouponCode,
                //    DiscountAmount = objList.DiscountAmount,
                //    MinAmount = objList.MinAmount
                //};
                _responseDto.Result = _mapper.Map<CouponDto>(objList);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon objList = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _responseDto.Result = _mapper.Map<CouponDto>(objList);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon objList = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(objList);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(objList);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon objList = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(objList);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(objList);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpDelete]
		[Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponId == id);
                _db.Coupons.Remove(obj);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
