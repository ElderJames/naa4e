using System.Collections.Generic;
using System.Linq;

namespace IBuyStuff.Domain.Repositories
{
    public interface IRepository<TAggregate>
    {
        /// <summary>
        /// Return a queryable object for all graphs. This method
        /// represents the foundation of LET (layered-expression-trees):
        /// you build one query through the layers (not tiers!)
        /// </summary>
        /// <returns>IQueryable (query, not data)</returns>
        IList<TAggregate> FindAll();

        /// <summary>
        /// Add an aggregate graph to the store.
        /// </summary>
        /// <param name="aggregate">Aggregate root object</param>
        /// <returns>True if successful; False otherwise</returns>
        bool Add(TAggregate aggregate);

        /// <summary>
        /// Save changes to an aggregate graph already in the store.
        /// </summary>
        /// <param name="aggregate">Aggregate root object</param>
        /// <returns>True if successful; False otherwise</returns>
        bool Save(TAggregate aggregate);

        /// <summary>
        /// Delete the entire graph rooted in the specified aggregate object. 
        /// Cascading rules must be set at the DB level.
        /// </summary>
        /// <param name="aggregate">Aggregate root object</param>
        /// <returns>True if successful; False otherwise</returns>
        bool Delete(TAggregate aggregate);

    }
}