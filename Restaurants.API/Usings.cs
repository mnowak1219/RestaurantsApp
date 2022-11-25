global using API_Restaurants.Authorization;
global using API_Restaurants.Entities;
global using API_Restaurants.Exceptions;
global using API_Restaurants.Middleware;
global using API_Restaurants.Models;
global using API_Restaurants.Models.Validators;
global using API_Restaurants.Services;
global using AutoMapper;
global using Bogus;
global using Bogus.Extensions;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.StaticFiles;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.FileProviders;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.IdentityModel.Tokens;
global using NLog.Web;
global using System;
global using System.Collections.Generic;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.IdentityModel.Tokens.Jwt;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text;