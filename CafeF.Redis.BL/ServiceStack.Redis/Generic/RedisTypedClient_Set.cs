//
// http://code.google.com/p/servicestack/wiki/ServiceStackRedis
// ServiceStack.Redis: ECMA CLI Binding to the Redis key-value storage system
//
// Authors:
//   Demis Bellot (demis.bellot@gmail.com)
//
// Copyright 2010 Liquidbit Ltd.
//
// Licensed under the same terms of Redis and ServiceStack: new BSD license.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Common.Extensions;
using ServiceStack.DesignPatterns.Model;

namespace ServiceStack.Redis.Generic
{
	internal partial class RedisTypedClient<T>
	{
		public IHasNamed<IRedisSet<T>> Sets { get; set; }

		public int Db
		{
			get { return client.Db; }
			set { client.Db = value; }
		}

		internal class RedisClientSets
			: IHasNamed<IRedisSet<T>>
		{
			private readonly RedisTypedClient<T> client;

			public RedisClientSets(RedisTypedClient<T> client)
			{
				this.client = client;
			}

			public IRedisSet<T> this[string setId]
			{
				get
				{
					return new RedisClientSet<T>(client, setId);
				}
				set
				{
					var col = this[setId];
					col.Clear();
					col.CopyTo(value.ToArray(), 0);
				}
			}
		}

		private HashSet<T> CreateHashSet(byte[][] multiDataList)
		{
			var results = new HashSet<T>();
			foreach (var multiData in multiDataList)
			{
				results.Add(DeserializeValue(multiData));
			}
			return results;
		}

		public List<T> GetSortedEntryValues(IRedisSet<T> fromSet, int startingFrom, int endingAt)
		{
			var sortOptions = new SortOptions { Skip = startingFrom, Take = endingAt, };
			var multiDataList = client.Sort(fromSet.Id, sortOptions);
			return CreateList(multiDataList);
		}

		public HashSet<T> GetAllItemsFromSet(IRedisSet<T> fromSet)
		{
			var multiDataList = client.SMembers(fromSet.Id);
			return CreateHashSet(multiDataList);
		}

		public void AddItemToSet(IRedisSet<T> toSet, T item)
		{
			client.SAdd(toSet.Id, SerializeValue(item));
		}

		public void RemoveItemFromSet(IRedisSet<T> fromSet, T item)
		{
			client.SRem(fromSet.Id, SerializeValue(item));
		}

		public T PopItemFromSet(IRedisSet<T> fromSet)
		{
			return DeserializeValue(client.SPop(fromSet.Id));
		}

		public void MoveBetweenSets(IRedisSet<T> fromSet, IRedisSet<T> toSet, T item)
		{
			client.SMove(fromSet.Id, toSet.Id, SerializeValue(item));
		}

		public int GetSetCount(IRedisSet<T> set)
		{
			return client.SCard(set.Id);
		}

		public bool SetContainsItem(IRedisSet<T> set, T item)
		{
			return client.SIsMember(set.Id, SerializeValue(item)) == 1;
		}

		public HashSet<T> GetIntersectFromSets(params IRedisSet<T>[] sets)
		{
			var multiDataList = client.SInter(sets.ConvertAll(x => x.Id).ToArray());
			return CreateHashSet(multiDataList);
		}

		public void StoreIntersectFromSets(IRedisSet<T> intoSet, params IRedisSet<T>[] sets)
		{
			client.SInterStore(intoSet.Id, sets.ConvertAll(x => x.Id).ToArray());
		}

		public HashSet<T> GetUnionFromSets(params IRedisSet<T>[] sets)
		{
			var multiDataList = client.SUnion(sets.ConvertAll(x => x.Id).ToArray());
			return CreateHashSet(multiDataList);
		}

		public void StoreUnionFromSets(IRedisSet<T> intoSet, params IRedisSet<T>[] sets)
		{
			client.SUnionStore(intoSet.Id, sets.ConvertAll(x => x.Id).ToArray());
		}

		public HashSet<T> GetDifferencesFromSet(IRedisSet<T> fromSet, params IRedisSet<T>[] withSets)
		{
			var multiDataList = client.SDiff(fromSet.Id, withSets.ConvertAll(x => x.Id).ToArray());
			return CreateHashSet(multiDataList);
		}

		public void StoreDifferencesFromSet(IRedisSet<T> intoSet, IRedisSet<T> fromSet, params IRedisSet<T>[] withSets)
		{
			client.SDiffStore(intoSet.Id, fromSet.Id, withSets.ConvertAll(x => x.Id).ToArray());
		}

		public T GetRandomItemFromSet(IRedisSet<T> fromSet)
		{
			return DeserializeValue(client.SRandMember(fromSet.Id));
		}

	    public T GetById(object id)
	    {
	        throw new NotImplementedException();
	    }

	    public IList<T> GetByIds(IEnumerable ids)
	    {
	        throw new NotImplementedException();
	    }

	    public void DeleteById(object id)
	    {
	        throw new NotImplementedException();
	    }

	    public void DeleteByIds(IEnumerable ids)
	    {
	        throw new NotImplementedException();
	    }
	}
}