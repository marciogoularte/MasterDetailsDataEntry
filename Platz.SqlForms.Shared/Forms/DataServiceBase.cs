﻿using Microsoft.EntityFrameworkCore;
using Platz.SqlForms.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platz.SqlForms
{
    public abstract class DataServiceBase
    {
        protected readonly IDataFieldProcessor _dataFieldProcessor;
        protected DataServiceFormBuilder _builder;

        public DataServiceBase()
        {
            _dataFieldProcessor = new DefaultDataFieldProcessor();
            _builder = new DataServiceFormBuilder();
            Define(_builder);
        }

        protected virtual void Define(DataServiceFormBuilder builder)
        {
        }

        public IList ExecuteListQuery(object[] parameters)
        {
            var result = _builder.ListQuery(parameters) as IList;
            return result;
        }

        public IEnumerable<DataField> GetDetailsFields()
        {
            var fields = new List<DataField>();
            // _builder.Builders.Where(b => b != _builder.Builders[_builder.MasterEntityIndex]).Single().Fields.OrderBy(f => f.Order);

            for (int i = 0; i < _builder.Builders.Count; i++)
            {
                if (i != _builder.MasterEntityIndex)
                {
                    fields.AddRange(_builder.Builders[i].Fields.OrderBy(f => f.Order));
                    // we expect only one non master Entity
                    break;
                }
            }

            // _fields = fields.ToDictionary(f => f.BindingProperty, f => f);

            if (fields.Any())
            {
                _dataFieldProcessor.PrepareFields(fields.ToList(), GetEntityType());
            }

            return fields;
        }

        private Type GetEntityType()
        {
            return _builder.Entities.First();
        }
    }

    public abstract class DataServiceBase<T> : DataServiceBase where T: DbContext
    {
        // protected DbContext Context { get; set; }

        protected T GetDbContext()
        {
            var context = Activator.CreateInstance<T>();
            return context;
        }

        
    }
}
