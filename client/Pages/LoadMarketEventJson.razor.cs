using System;
using Radzen;
using System.Text.Json;
using CryptobotUi.Client.Model;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptobotUi.Pages
{
    public partial class LoadMarketEventJsonComponent
    {        
        protected string ValidationError { get; private set; }

        protected string MarketEventJson { get; set; }

        protected void LoadMarketEventJson() 
        {
            try 
            {
                var marketEvent = JsonSerializer.Deserialize<MarketEvent>(MarketEventJson, new JsonSerializerOptions {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    NumberHandling = JsonNumberHandling.Strict,
                });
                
                System.Collections.Generic.ICollection<ValidationResult> results = new List<ValidationResult>();
                if (Validator.TryValidateObject(marketEvent, new ValidationContext(marketEvent), results))
                { 
                    DialogService.Close(marketEvent);
                }
                else 
                {
                    var errors = new StringBuilder();
                    foreach (var validationResult in results)
                    {
                        errors.AppendLine(validationResult.ToString());
                    }
                    this.ValidationError = errors.ToString();
                }
            }
            catch (ArgumentNullException)
            {
                this.ValidationError = "Please enter some JSON value.";
            }
            catch (JsonException jx)
            {
                this.ValidationError = $"Error parsing JSON into Market event: \n {jx.LineNumber}: {jx.Message}";
            }
            catch (Exception ex) 
            {
                this.ValidationError = $"Error parsing JSON: {ex}";
            }
        }
    }
}
