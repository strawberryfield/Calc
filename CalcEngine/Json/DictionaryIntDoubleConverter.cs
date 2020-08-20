// copyright (c) 2020 Roberto Ceccarelli - CasaSoft
// http://strawberryfield.altervista.org 
// 
// This file is part of CasaSoft Calc
// 
// CasaSoft Calc is free software: 
// you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// CasaSoft Calc is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with CasaSoft Calc.  
// If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Casasoft.Calc.Json
{
    public class DictionaryIntDoubleConverter :
        JsonConverter<Dictionary<int, double>>
    {
        public override Dictionary<int, double> Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            Dictionary<int, double> dictionary = new Dictionary<int, double>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                // Get the key.
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string propertyName = reader.GetString();

                int key;
                if (!int.TryParse(propertyName, out key))
                {
                    throw new JsonException(
                        $"Unable to convert \"{propertyName}\" to int.");
                }

                // Get the value.
                double v;
                reader.Read();
                string propertyValue = reader.GetString();
                if (!double.TryParse(propertyValue, NumberStyles.Float, CultureInfo.InvariantCulture, out v))
                {
                    throw new JsonException(
                        $"Unable to convert \"{propertyValue}\" to double.");
                }

                // Add to dictionary.
                dictionary.Add(key, v);
            }

            throw new JsonException();
        }

        public override void Write(
            Utf8JsonWriter writer,
            Dictionary<int, double> dictionary,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (KeyValuePair<int, double> kvp in dictionary)
            {
                writer.WriteString(kvp.Key.ToString(), kvp.Value.ToString(CultureInfo.InvariantCulture));
            }

            writer.WriteEndObject();
        }
    }
}
