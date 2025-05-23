using System;
using System.Collections.Generic;
using System.Data.Entity; // Required for State
using System.Linq;

namespace Assignment2.DAL.Repositories
{
    // Repository for Agent entity operations
    public class AgentRepository : IDisposable
    {
        private SE_Assignment2_DBEntities context; // Adjust name if needed

        public AgentRepository()
        {
            context = new SE_Assignment2_DBEntities(); // Adjust name if needed
        }

        // Get all agents
        public List<Agent> GetAllAgents()
        {
            // Include related Orders if needed later, for now just Agents
            // return context.Agents.Include(a => a.Orders).ToList();
            return context.Agents.ToList();
        }

        // Get a single agent by ID
        public Agent GetAgentById(int agentId)
        {
            return context.Agents.Find(agentId);
        }

        // Add a new agent
        public void AddAgent(Agent agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent));
            }
            context.Agents.Add(agent);
            context.SaveChanges();
        }

        // Update an existing agent
        public void UpdateAgent(Agent agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent));
            }
            context.Entry(agent).State = EntityState.Modified;
            context.SaveChanges();
        }

        // Delete an agent by ID
        public void DeleteAgent(int agentId)
        {
            Agent agentToDelete = context.Agents.Find(agentId);
            if (agentToDelete != null)
            {
                context.Agents.Remove(agentToDelete);
                context.SaveChanges();
            }
        }

        // Implement IDisposable
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
