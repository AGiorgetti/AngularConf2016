using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace WebApplication.Monitoring
{
	/// <summary>
	/// A JSON serialization wrapper properly configured between server and client
	/// </summary>
	public static class JsonSerializerHelper
	{
		private static readonly JsonSerializerSettings Settings;

		static JsonSerializerHelper()
		{
			Settings = new JsonSerializerSettings();
			JsonSerializerSettingsConfigurator.Configure(Settings);
		}

		public static string SerializeObject(object value)
		{
			return JsonConvert.SerializeObject(value, Settings);
		}

		public static object DeserializeObject(string json)
		{
			if (json == null)
				return null;
			return JsonConvert.DeserializeObject(json, Settings);
		}

		public static T DeserializeObject<T>(string json)
		{
			if (json == null)
				return default(T);
			return JsonConvert.DeserializeObject<T>(json, Settings);
		}
	}

	/// <summary>
	/// Used to configure serialization to be used in SignalR and Web.Api
	/// </summary>
	public static class JsonSerializerSettingsConfigurator
	{
		// ISO DateTime for SignalR & Web.Api
		// json does not have a type to express dates, they are encoded as strings
		public static void Configure(JsonSerializerSettings settings)
		{
			// Used to handle serialization and deserialization with protected constructurs
			// non sembra funzionare con SignalR: ci vogliono oggetti con costruttore pubblico senza parametri
			settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

			// let's use Upper Camel Case / Pascal Case for everything
			settings.ContractResolver = new DefaultContractResolver();

			settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
			settings.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			// settings.NullValueHandling = NullValueHandling.Ignore
#if !DEBUG
            settings.Formatting = Formatting.None;
#endif
		}
	}
}