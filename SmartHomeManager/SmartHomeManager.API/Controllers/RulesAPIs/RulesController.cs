﻿using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Services;
using SmartHomeManager.Domain.Common;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHomeManager.API.Controllers.RulesAPIs;

[Route("api/[controller]")]
[ApiController]
public class RulesController : ControllerBase
{
    private readonly RuleServices _registerRuleService;
    private readonly GetRulesServices _getRulesServices;

    public RulesController(IGenericRepository<Rule> ruleRepository)
    {
        _registerRuleService = new(ruleRepository);
    }

    // GET: api/Rules/GetAllRules
    [HttpGet("GetAllRules")]
    public async Task<IEnumerable<RuleRequest>> GetAllRules()
    {
        var rules = await _registerRuleService.GetAllRulesAsync();
        var resp = rules.Select(rule => new RuleRequest
        {
            RuleId = rule.RuleId,
            ScenarioId = rule.ScenarioId,
            ConfigurationValue = rule.ConfigurationValue,
            ActionTrigger = rule.ActionTrigger,
            ScheduleName = rule.ScheduleName,
            StartTime = Convert.ToDateTime(rule.StartTime),
            EndTime = Convert.ToDateTime(rule.EndTime),
            DeviceId = rule.DeviceId
        }).ToList();
        return resp;
    }

    // GET api/Rules/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Rule>> GetRule([FromBody] Guid id)
    {
        var rule = await _registerRuleService.GetRuleByIdAsync(id);
        if(rule != null)
        {
            return StatusCode(200, rule);
        }
        return StatusCode(404, "rule not exist");
    }

    // POST api/Rules
    [HttpPost("CreateRule")]
    public async Task<ActionResult> CreateRule([FromBody] RuleRequest ruleRequest)
    {
        var rule = new Rule
        {
            RuleId = ruleRequest.RuleId,
            ScenarioId = ruleRequest.ScenarioId,
            ConfigurationValue = ruleRequest.ConfigurationValue,
            ActionTrigger = ruleRequest.ActionTrigger,
            ScheduleName = ruleRequest.ScheduleName,
            StartTime = Convert.ToDateTime(ruleRequest.StartTime),
            EndTime = Convert.ToDateTime(ruleRequest.EndTime),
            DeviceId = ruleRequest.DeviceId
        };
        await _registerRuleService.CreateRuleAsync(rule);
        /*
         * 
         * INTERFACE TO BE CALLED: 
         * void informRuleChanges(Guid ScenarioId)
         * 
         */
        return StatusCode(200, ruleRequest);
    }

    // PUT api/Rules/5
    [HttpPut("EditRule")]
    public async Task<ActionResult> EditRule(Rule rule)
    {
        await _registerRuleService.EditRuleAsync(rule);
        /*
         * 
         * INTERFACE TO BE CALLED: 
         * void informRuleChanges(Guid ScenarioId)
         * 
         */
        return StatusCode(200, rule);
    }

    // DELETE api/Rules/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRule([FromBody] Guid id)
    {
        var rule = await _registerRuleService.GetRuleByIdAsync(id);
        if (rule != null)
        {
            await _registerRuleService.DeleteRuleByIdAsync(id);
            /*
             * 
             * INTERFACE TO BE CALLED: 
             * void informRuleChanges(Guid ScenarioId)
             * 
             */ 
            return StatusCode(200, rule);
        }
        return StatusCode(404, "rule not exist");
    }
}

