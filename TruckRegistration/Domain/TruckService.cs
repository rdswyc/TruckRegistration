using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Logging;
using TruckRegistration.Exceptions;
using TruckRegistration.Models;

namespace TruckRegistration.Domain
{
    public class TruckService
    {
        private readonly ILogger<TruckService> _logger;
        private readonly IRepository<Truck> _repos;

        public TruckService(
            ILogger<TruckService> logger,
            IRepository<Truck> repository
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repos = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual TruckViewModel Add(TruckViewModel model)
        {
            _logger.LogInformation("Adding a truck to the database.", model);

            Truck entity;

            try
            {
                var truck = new TruckCreator(model.Model, model.ProductionYear, model.ModelYear);
                entity = _repos.Add(truck);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding truck.", ex.Message, ex.InnerException);

                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _logger.LogInformation($"Successfully added truck.", entity);

            return new TruckViewModel
            {
                Id = entity.Id,
                Model = entity.Model,
                ModelYear = entity.ModelYear,
                ProductionYear = entity.ProductionYear
            };
        }

        public virtual void Delete(int id)
        {
            _logger.LogInformation($"Deleting a truck {id} from the database.");

            if (!_repos.Exists(id))
            {
                _logger.LogError($"Truck {id} does not exist.");

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                _repos.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting truck.", ex.Message, ex.InnerException);

                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _logger.LogInformation($"Successfully deleted truck {id}.");
        }

        public virtual void Edit(int id, TruckViewModel model)
        {
            _logger.LogInformation($"Editing truck {id} from the database.", model);

            if (!_repos.Exists(id))
            {
                _logger.LogError($"Truck {id} does not exist.");

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                var truck = new TruckCreator(id, model.Model, model.ProductionYear, model.ModelYear);
                _repos.Edit(truck);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error editing truck.", ex.Message, ex.InnerException);

                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _logger.LogInformation($"Successfully edited truck {id}.");
        }

        public virtual TruckViewModel Get(int id)
        {
            _logger.LogInformation($"Getting truck {id} from the database.");

            Truck entity;

            if (!_repos.Exists(id))
            {
                _logger.LogError($"Truck {id} does not exist.");

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                entity = _repos.Get(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting truck.", ex.Message, ex.InnerException);

                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _logger.LogInformation($"Successfully got truck.", entity);

            return new TruckViewModel
            {
                Id = entity.Id,
                Model = entity.Model,
                ModelYear = entity.ModelYear,
                ProductionYear = entity.ProductionYear
            };
        }

        public virtual IEnumerable<TruckViewModel> GetAll()
        {
            _logger.LogInformation($"Getting all the trucks in the database.");

            IEnumerable<Truck> entities;

            try
            {
                entities = _repos.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting trucks.", ex.Message, ex.InnerException);

                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _logger.LogInformation($"Successfully got trucks.", entities);

            return entities.Select(entity => new TruckViewModel
            {
                Id = entity.Id,
                Model = entity.Model,
                ModelYear = entity.ModelYear,
                ProductionYear = entity.ProductionYear
            });
        }
    }
}
