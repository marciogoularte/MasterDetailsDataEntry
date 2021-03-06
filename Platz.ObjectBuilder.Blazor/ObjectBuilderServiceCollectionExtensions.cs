﻿using Microsoft.Extensions.DependencyInjection;
using Platz.ObjectBuilder.Blazor;
using Platz.ObjectBuilder.Blazor.Controllers;
using Platz.ObjectBuilder.Blazor.Controllers.Logic;
using Platz.ObjectBuilder.Blazor.Controllers.Validation;
using Platz.ObjectBuilder.Engine;
using Platz.ObjectBuilder.Expressions;
using Plk.Blazor.DragDrop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Platz.ObjectBuilder
{
    public static class ObjectBuilderServiceCollectionExtensions
    {
        public static IServiceCollection AddPlatzObjectBuilder([NotNullAttribute] this IServiceCollection services)
        {
            services.AddScoped(typeof(DragDropService<>));
            services.AddTransient<IObjectResolver, SqlJsonObjectResolver>();
            services.AddTransient<IObjectBuilderRuleFactory, ObjectBuilderRuleFactory>();
            services.AddTransient<ISqlExpressionEngine, SqlExpressionEngine>();
            services.AddTransient<IQueryBuilderEngine, QueryBuilderEngine>();
            services.AddTransient<IQueryController, EntityFrameworkQueryController>();
            services.AddTransient<ISchemaController, SchemaController>();
            services.AddTransient<SchemaTableDesignController, SchemaTableDesignController>();
            services.AddSingleton<IStoreSchemaStorage, FileStoreSchemaStorage>();
            return services;
        }
    }
}
