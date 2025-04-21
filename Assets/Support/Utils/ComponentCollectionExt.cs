using System.Collections.Generic;
using UnityEngine;

namespace UltimateHelper
{
    public static class ComponentCollectionExt
    {
        public static void DestroyGameObjectAll<T>( this T[] self ) where T : Component
        {
            if ( self == null ) return;

            for ( var i = 0; i < self.Length; i++ )
            {
                Object.Destroy( self[ i ].gameObject );
                self[ i ] = null;
            }
        }

        public static void DestroyGameObjectImmediateAll<T>( this T[] self ) where T : Component
        {
            if ( self == null ) return;

            for ( var i = 0; i < self.Length; i++ )
            {
                Object.DestroyImmediate( self[ i ].gameObject );
                self[ i ] = null;
            }
        }

        public static void DestroyGameObjectAll<T>( this List<T> self ) where T : Component
        {
            if ( self == null ) return;

            for ( var i = 0; i < self.Count; i++ )
            {
                Object.Destroy( self[ i ].gameObject );
                self[ i ] = null;
            }

            self.Clear();
        }

        public static void DestroyGameObjectImmediateAll<T>( this List<T> self ) where T : Component
        {
            if ( self == null ) return;

            for ( var i = 0; i < self.Count; i++ )
            {
                Object.DestroyImmediate( self[ i ].gameObject );
                self[ i ] = null;
            }

            self.Clear();
        }

        public static void SetActiveAll<T>( this IReadOnlyList<T> self, bool value ) where T : Component
        {
            foreach ( var x in self )
            {
                x.gameObject.SetActive( value );
            }
        }
    }
}