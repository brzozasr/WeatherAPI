namespace WeatherAPI.Extensions
{
    public static class WindDirection
    {
        public static string DirectionTxt(this int direction)
        {
            var dirTxt = string.Empty;
            
            if (direction is >= 348 and <= 360 or >= 0 and <= 11)
            {
                dirTxt = "N";
            }
            else if (direction is >= 12 and <= 34)
            {
                dirTxt = "NNE";
            }
            else if (direction is >= 35 and <= 56)
            {
                dirTxt = "NE";
            }
            else if (direction is >= 57 and <= 79)
            {
                dirTxt = "ENE";
            }
            else if (direction is >= 80 and <= 101)
            {
                dirTxt = "E";
            }
            else if (direction is >= 102 and <= 124)
            {
                dirTxt = "ESE";
            }
            else if (direction is >= 125 and <= 146)
            {
                dirTxt = "SE";
            }
            else if (direction is >= 147 and <= 169)
            {
                dirTxt = "SSE";
            }
            else if (direction is >= 170 and <= 191)
            {
                dirTxt = "S";
            }
            else if (direction is >= 192 and <= 214)
            {
                dirTxt = "SSW";
            }
            else if (direction is >= 215 and <= 236)
            {
                dirTxt = "SW";
            }
            else if (direction is >= 237 and <= 259)
            {
                dirTxt = "WSW";
            }
            else if (direction is >= 260 and <= 281)
            {
                dirTxt = "W";
            }
            else if (direction is >= 282 and <= 304)
            {
                dirTxt = "WNW";
            }
            else if (direction is >= 305 and <= 326)
            {
                dirTxt = "NW";
            }
            else if (direction is >= 327 and <= 347)
            {
                dirTxt = "NNW";
            }

            return dirTxt;
        }

        public static string DirectionArrow(this int direction)
        {
            var dirImg = string.Empty;
            
            if (direction is >= 338 and <= 360 or >= 0 and <= 23)
            {
                dirImg = "n";
            } 
            else if (direction is >= 24 and <= 68)
            {
                dirImg = "ne";
            }
            else if (direction is >= 69 and <= 113)
            {
                dirImg = "e";
            }
            else if (direction is >= 114 and <= 158)
            {
                dirImg = "se";
            }
            else if (direction is >= 159 and <= 203)
            {
                dirImg = "s";
            }
            else if (direction is >= 204 and <= 248)
            {
                dirImg = "sw";
            }
            else if (direction is >= 249 and <= 293)
            {
                dirImg = "w";
            }
            else if (direction is >= 294 and <= 337)
            {
                dirImg = "nw";
            }

            return dirImg;
        }
    }
}
