using System;
using System.Collections.Generic;
using System.Linq;
using Green.Core;
using Green.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Green.Data
{
	/// <summary>
	/// Entity Framework repository
	/// </summary>
    public partial class EfRepository<T> : IRepository<T> where T : class
	{
		#region Fields

        private readonly GreenObjectContext _context;
		private DbSet<T> _entities;

		#endregion

		#region Ctor

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="context">Object context</param>
		public EfRepository(GreenObjectContext context)
		{
            this._context = context;
		}

		#endregion

		#region Utilities

		/// <summary>
		/// Get full error
		/// </summary>
		/// <param name="exc">Exception</param>
		/// <returns>Error</returns>
		protected string GetFullErrorText(Exception exc)
		{
            return exc.Message;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Get entity by identifier
		/// </summary>
		/// <param name="id">Identifier</param>
		/// <returns>Entity</returns>
		public virtual T GetById(object id)
		{
			return this.Entities.Find(id);
		}

		/// <summary>
		/// Insert entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Insert(T entity)
		{
			try
			{
				if (entity == null)
					throw new ArgumentNullException(nameof(entity));

				this.Entities.Add(entity);

				this._context.SaveChanges();
			}
			catch (Exception dbEx)
			{
				throw new Exception(GetFullErrorText(dbEx), dbEx);
			}
		}

		/// <summary>
		/// Insert entities
		/// </summary>
		/// <param name="entities">Entities</param>
		public virtual void Insert(IEnumerable<T> entities)
		{
			try
			{
				if (entities == null)
					throw new ArgumentNullException(nameof(entities));

				foreach (var entity in entities)
					this.Entities.Add(entity);
				this._context.SaveChanges();
			}
			catch (Exception dbEx)
			{
				throw new Exception(GetFullErrorText(dbEx), dbEx);
			}
		}

		/// <summary>
		/// Update entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Update(T entity)
		{
			try
			{
				if (entity == null)
					throw new ArgumentNullException(nameof(entity));

				this._context.SaveChanges();
			}
			catch (Exception dbEx)
			{
				throw new Exception(GetFullErrorText(dbEx), dbEx);
			}
		}

		/// <summary>
		/// Update entities
		/// </summary>
		/// <param name="entities">Entities</param>
		public virtual void Update(IEnumerable<T> entities)
		{
			try
			{
				if (entities == null)
					throw new ArgumentNullException(nameof(entities));

				this._context.SaveChanges();
			}
			catch (Exception dbEx)
			{
				throw new Exception(GetFullErrorText(dbEx), dbEx);
			}
		}

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Delete(T entity)
		{
			try
			{
				if (entity == null)
					throw new ArgumentNullException(nameof(entity));

				this.Entities.Remove(entity);

				this._context.SaveChanges();
			}
			catch (Exception dbEx)
			{
				throw new Exception(GetFullErrorText(dbEx), dbEx);
			}
		}

		/// <summary>
		/// Delete entities
		/// </summary>
		/// <param name="entities">Entities</param>
		public virtual void Delete(IEnumerable<T> entities)
		{
			try
			{
				if (entities == null)
					throw new ArgumentNullException(nameof(entities));

				foreach (var entity in entities)
					this.Entities.Remove(entity);

				this._context.SaveChanges();
			}
			catch (Exception dbEx)
			{
				throw new Exception(GetFullErrorText(dbEx), dbEx);
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets a table
		/// </summary>
		public virtual IQueryable<T> Table
		{
			get
			{
				return this.Entities;
			}
		}

		/// <summary>
		/// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
		/// </summary>
		public virtual IQueryable<T> TableNoTracking
		{
			get
			{
				return this.Entities.AsNoTracking();
			}
		}

		/// <summary>
		/// Entities
		/// </summary>
		protected virtual DbSet<T> Entities
		{
			get
			{
                if (_entities == null)
                    _entities = _context.Set<T>();
				return _entities;
			}
		}

		#endregion
	}
}