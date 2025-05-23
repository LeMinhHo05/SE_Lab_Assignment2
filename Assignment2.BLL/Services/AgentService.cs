using Assignment2.DAL; // Access Agent entity
using Assignment2.DAL.Repositories; // Access AgentRepository
using System;
using System.Collections.Generic;

namespace Assignment2.BLL.Services
{
    // Service layer for Agent operations
    public class AgentService : IDisposable
    {
        private AgentRepository _agentRepository;

        public AgentService()
        {
            _agentRepository = new AgentRepository();
        }

        // Get all agents
        public List<Agent> GetAllAgents()
        {
            return _agentRepository.GetAllAgents();
        }

        // Get agent by ID
        public Agent GetAgentById(int agentId)
        {
            if (agentId <= 0) return null;
            return _agentRepository.GetAgentById(agentId);
        }

        // Add a new agent
        public bool AddAgent(string agentName, string address)
        {
            // Validation: Agent name is required
            if (string.IsNullOrWhiteSpace(agentName))
            {
                return false;
            }

            Agent newAgent = new Agent
            {
                AgentName = agentName,
                Address = address // Address can be null/empty
            };

            try
            {
                _agentRepository.AddAgent(newAgent);
                return true;
            }
            catch (Exception ex)
            {
                // Log exception ex
                return false;
            }
        }

        // Update an existing agent
        public bool UpdateAgent(int agentId, string agentName, string address)
        {
            if (agentId <= 0 || string.IsNullOrWhiteSpace(agentName))
            {
                return false;
            }

            Agent agentToUpdate = _agentRepository.GetAgentById(agentId);
            if (agentToUpdate == null)
            {
                return false; // Agent not found
            }

            agentToUpdate.AgentName = agentName;
            agentToUpdate.Address = address;

            try
            {
                _agentRepository.UpdateAgent(agentToUpdate);
                return true;
            }
            catch (Exception ex)
            {
                // Log exception ex
                return false;
            }
        }

        // Delete an agent
        public bool DeleteAgent(int agentId)
        {
            if (agentId <= 0)
            {
                return false;
            }

            try
            {
                // IMPORTANT: Add business logic check here!
                // Can we delete an agent if they have existing orders? Usually NO.
                // You would query the Order table/repository first.
                // For now, we proceed, but this might fail due to FK constraints in DB.
                // Example check (requires OrderRepository/Service):
                // if (_orderService.AgentHasOrders(agentId)) return false;

                _agentRepository.DeleteAgent(agentId);
                return true;
            }
            catch (Exception ex) // Catches DB errors like FK violation
            {
                // Log exception ex
                return false;
            }
        }

        // Implement IDisposable
        public void Dispose()
        {
            _agentRepository?.Dispose();
        }
    }
}