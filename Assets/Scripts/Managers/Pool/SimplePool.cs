using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// Author: Suleyman Yasir Kula
public class SimplePool<T> where T : class
{
	private Stack<T> pool = null;

	public T Blueprint { get; set; }

	public Func<T, T> CreateFunction;

	public Action<T> OnPush, OnPop;

	public SimplePool( Func<T, T> CreateFunction = null, Action<T> OnPush = null, Action<T> OnPop = null )
	{
		pool = new Stack<T>();

		this.CreateFunction = CreateFunction;
		this.OnPush = OnPush;
		this.OnPop = OnPop;
	}

	public SimplePool( T blueprint, Func<T, T> CreateFunction = null, Action<T> OnPush = null, Action<T> OnPop = null )
							: this( CreateFunction, OnPush, OnPop )
	{
		Blueprint = blueprint;
	}

	public bool Populate( int count )
	{
		return Populate( Blueprint, count );
	}

	public bool Populate( T blueprint, int count )
	{
		if( count <= 0 )
			return true;

		T obj = NewObject( blueprint );
		if( obj == null )
			return false;

		Push( obj );

		for( int i = 1; i < count; i++ )
		{
			Push( NewObject( blueprint ) );
		}

		return true;
	}

	public T Pop()
	{
		T objToPop;

		if( pool.Count == 0 )
		{
			objToPop = NewObject( Blueprint );
		}
		else
		{
			objToPop = pool.Pop();
			while( objToPop == null )
			{
				if( pool.Count > 0 )
					objToPop = pool.Pop();
				else
				{
					objToPop = NewObject( Blueprint );
					break;
				}
			}
		}

		if( OnPop != null )
			OnPop( objToPop );

		return objToPop;
	}

	public T[] Pop( int count )
	{
		if( count <= 0 )
			return new T[0];

		T[] result = new T[count];
		for( int i = 0; i < count; i++ )
			result[i] = Pop();

		return result;
	}

	public void Push( T obj )
	{
		if( obj == null ) return;

		if( OnPush != null )
			OnPush( obj );

		pool.Push( obj );
	}

	public void Push( IEnumerable<T> objects )
	{
		if( objects == null ) return;

		foreach( T obj in objects) {
			if (obj == null)
				continue;
			Push( obj );
		}
	}

	public void Clear( bool destroyObjects = true )
	{
		if( destroyObjects )
		{
			foreach( T item in pool )
			{
				Object.Destroy( item as Object );
			}
		}

		pool.Clear();
	}

	private T NewObject( T blueprint )
	{
		if( CreateFunction != null )
			return CreateFunction( blueprint );

		if( blueprint == null || !( blueprint is Object ) )
			return null;

		return Object.Instantiate( blueprint as Object ) as T;
	}
}
