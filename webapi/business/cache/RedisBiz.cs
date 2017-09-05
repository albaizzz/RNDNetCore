using System;
using StackExchange.Redis;

namespace webapi.business.cache
{
    public class RedisBiz
    {
        private string _url;
        private int _lifetime;
        private string _redisOn;

       public RedisBiz(string url, int lifetime, string redisOn)
        {
            _url = url;
            _lifetime = lifetime;
            _redisOn = redisOn;
        }
        public bool SetRedis(string keyString, string objString)
        {
            if (_redisOn != "true") return true;

           try
            {
                var objRedis = new RedisCache(_url);
                var key = keyString;
                var objectString = objString;

               var objLower = objectString.ToLower();
                if (objLower.IndexOf("error") >= 0)
                {
                    return true;
                }
                else
                {
                    return objRedis.SetData(_lifetime, key, objectString);
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            
       }

       public string GetRedis(string keyString)
        {
            try
            {
                if (_redisOn != "true") return null;

               var objRedis = new RedisCache(_url);
                var key = keyString;
                return objRedis.GetData(key);
              
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
       }

       public bool RemoveRedis(string keyString)
        {
            try
            {
                if (_redisOn != "true") return false;

               var objRedis = new RedisCache(_url);
                return objRedis.RemoveData(keyString);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}