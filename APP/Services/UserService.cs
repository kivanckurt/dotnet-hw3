using APP.Domain;
using APP.Models;
using CORE.APP.Models;
using CORE.APP.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Services
{
    public class UserService : Service<User>, IService<UserRequest, UserResponse>
    {
        public UserService(DbContext db) : base(db)
        {
        }

        /// <summary>
        /// Gets a list of all users.
        /// </summary>
        public List<UserResponse> List()
        {
            return Query().Select(u => new UserResponse
            {
                Id = u.Id,
                Guid = u.Guid,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Gender = u.Gender,
                BirthDate = u.BirthDate,
                RegistrationDate = u.RegistrationDate,
                Score = u.Score,
                IsActive = u.IsActive,
                Address = u.Address,

            }).ToList();
        }

        /// <summary>
        /// Gets a single user by their ID.
        /// </summary>
        public UserResponse Item(int id)
        {
            var entity = Query().SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return null;

            return new UserResponse
            {
                Id = entity.Id,
                Guid = entity.Guid,
                UserName = entity.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Gender = entity.Gender,
                BirthDate = entity.BirthDate,
                RegistrationDate = entity.RegistrationDate,
                Score = entity.Score,
                IsActive = entity.IsActive,
                Address = entity.Address,

            };
        }

        /// <summary>
        /// Gets a UserRequest DTO for editing.
        /// </summary>
        public UserRequest Edit(int id)
        {
            var entity = Query().SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return null;

            return new UserRequest
            {
                Id = entity.Id,
                UserName = entity.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Gender = entity.Gender,
                BirthDate = entity.BirthDate,
                Address = entity.Address,
                Password = entity.Password,

                IsActive = entity.IsActive
                // Deliberately not mapping Password
            };
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        public CommandResponse Create(UserRequest request)
        {
            if (Query().Any(u => u.UserName == request.UserName.Trim()))
                return Error("User with the same username already exists!");



            var entity = new User
            {
                UserName = request.UserName.Trim(),
                Password = request.Password, 
                FirstName = request.FirstName?.Trim(),
                LastName = request.LastName?.Trim(),
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                Address = request.Address?.Trim(),

                IsActive = request.IsActive
                // RegistrationDate and Score are set by the User constructor
            };

            Create(entity);
            return Success("User created successfully.", entity.Id);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        public CommandResponse Update(UserRequest request)
        {
            if (Query().Any(u => u.Id != request.Id && u.UserName == request.UserName.Trim()))
                return Error("User with the same username already exists!");

            var entity = Query().SingleOrDefault(u => u.Id == request.Id);
            if (entity is null)
                return Error("User not found!");

            // Map properties
            entity.UserName = request.UserName.Trim();
            entity.FirstName = request.FirstName?.Trim();
            entity.LastName = request.LastName?.Trim();
            entity.Gender = request.Gender;
            entity.BirthDate = request.BirthDate;
            entity.Address = request.Address?.Trim();
            entity.IsActive = request.IsActive;

            // Only update password if a new one is provided
            if (!string.IsNullOrWhiteSpace(request.Password))
            {

                entity.Password = request.Password;
            }

            Update(entity);
            return Success("User updated successfully.", entity.Id);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        public CommandResponse Delete(int id)
        {
            var entity = Query().SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return Error("User not found!");

            Delete(entity);
            return Success("User deleted successfully.", entity.Id);
        }
    }
}
