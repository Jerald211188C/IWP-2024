Shader "Custom/ToonOutlineWithTexture"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _OutlineTex ("Outline Texture", 2D) = "white" {} // Texture for the outline
        _Color ("Main Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Tint", Color) = (1,0,0,1) // Default: Red
        _OutlineWidth ("Outline Width", Range(0.001, 0.03)) = 0.005
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" "RenderType" = "Transparent" }
        Cull Off

        // --- OUTLINE PASS ---
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "ForwardBase" }
            Cull Front
            ZWrite On
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float _OutlineWidth;
            float4 _OutlineColor;
            sampler2D _OutlineTex;

            v2f vert(appdata v)
            {
                v2f o;
                
                // Get world normal
                float3 normal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                
                // Scale outline thickness based on camera distance
                float camDist = distance(mul(unity_ObjectToWorld, v.vertex).xyz, _WorldSpaceCameraPos);
                v.vertex.xyz += normal * _OutlineWidth * camDist * 0.1;
                
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv; // Pass UV for texture sampling
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 outlineTexColor = tex2D(_OutlineTex, i.uv); // Sample the outline texture
                return outlineTexColor * _OutlineColor; // Multiply texture with tint color
            }

            ENDCG
        }

        // --- MAIN PASS ---
        Pass
        {
            Name "MAIN"
            Tags { "LightMode" = "ForwardBase" }
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv) * _Color; // Apply main texture
            }

            ENDCG
        }
    }
}
