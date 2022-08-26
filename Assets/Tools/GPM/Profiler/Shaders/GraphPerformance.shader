Shader "GPM/Profiler/GraphPerformance"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0

        _ScriptColor("Script Color", Color) = (1,1,0,1)
        _RenderColor("Render Color", Color) = (0,1,0,1)
    }

        SubShader
        {           
            Tags
            { 
                "Queue"="Transparent" 
                "IgnoreProjector"="True" 
                "RenderType"="Transparent" 
                "PreviewType"="Plane"
                "CanUseSpriteAtlas"="True"
            }

            Cull Off
            Lighting Off
            ZWrite Off
            ZTest Off
            Blend One OneMinusSrcAlpha

            Pass
            {
                Name "Default"
                CGPROGRAM

                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile _ PIXELSNAP_ON
                
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex    : POSITION;
                    float4 color     : COLOR;
                    float2 texcoord  : TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex    : SV_POSITION;
                    fixed4 color     : COLOR;
                    float2 texcoord  : TEXCOORD0;
                };

                fixed4 _Color;

                v2f vert(appdata_t IN)
                {
                    v2f OUT;
                    OUT.vertex = UnityObjectToClipPos(IN.vertex);
                    OUT.texcoord = IN.texcoord;
                    OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                    OUT.vertex = UnityPixelSnap(OUT.vertex);
                #endif

                    return OUT;
                }

                sampler2D _MainTex;
                sampler2D _AlphaTex;
                float _AlphaSplitEnabled;

                fixed4 SampleSpriteTexture(float2 uv)
                {
                    fixed4 color = tex2D(_MainTex, uv);

                #if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
                    if (_AlphaSplitEnabled)
                        color.a = tex2D(_AlphaTex, uv).r;
                #endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

                    return color;
                }

                fixed4 _ScriptColor;
                fixed4 _RenderColor;

				uniform float _ScriptValues[64];
				uniform float _RenderValues[64];

                uniform float _GraphValues_Length;

                fixed4 frag(v2f IN) : SV_Target
                {
                    fixed4 color = IN.color;

                    fixed xCoord = IN.texcoord.x;
                    fixed yCoord = IN.texcoord.y;

                    float cpuValue = _ScriptValues[floor(xCoord * _GraphValues_Length)];
					float gpuValue = _RenderValues[floor(xCoord * _GraphValues_Length)];
					
					if (yCoord < gpuValue && gpuValue > 0)
					{
						color *= _RenderColor;
					}
					else if (yCoord < cpuValue && cpuValue > 0)
					{
						color *= _ScriptColor;
					}
					else
					{
						color.a = 0;
					}

                    fixed4 c = SampleSpriteTexture(IN.texcoord) * color;

                    c.rgb *= c.a;

                    return c;
                }

                ENDCG
            }
        }
}