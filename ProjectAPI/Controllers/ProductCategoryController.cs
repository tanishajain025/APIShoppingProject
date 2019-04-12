﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace ProjectAPI.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        //ShopDataDbContext context = new ShopDataDbContext();
        private readonly ShopDataDbContext _context;

        public ProductCategoryController(ShopDataDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<ProductCategory> pc = await _context.Categories.ToListAsync();
            if(pc!=null)
            {
                return Ok(pc);
            }
            else
            {
                return NotFound();
            }
           
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var Categories = await _context.Categories.FindAsync(id);
            if (Categories == null)
            {
                return NotFound();
            }

            return Ok(Categories);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductCategory category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {

                try
                {
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(Get), new { id = category.ProductCategoryId }, category);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id==null)
            {
                return BadRequest();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody]ProductCategory category)
        {
            if(id==null)
            {
                return BadRequest();
            }
            if (id != category.ProductCategoryId)
            {
                return NotFound();

            }
            _context.Entry(category).State = EntityState.Modified;
            
                await _context.SaveChangesAsync();
           
            return NoContent();
        }

    }
}