Shader "GPM/Profiler/GraphMemory"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0

		_TotalReservedColor("Total Reserved Color", Color) = (0,0,1,1)
		_TotalAllocatedColor("Total Allocated Color", Color) = (0,1,0,1)
		_GfxColor("GFX Color", Color) = (1,0,1,1)
		_GCHeapColor("GC Heap Color", Color) = (0,1,1,1)
		_GCUsedColor("GC Used Color", Color) = (1,1,0,1)
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

                fixed4 _TotalReservedColor;
                fixed4 _TotalAllocatedColor;
				fixed4 _GfxColor;
				fixed4 _GCHeapColor;
				fixed4 _GCUsedColor;

                uniform float Average;

                // NOTE: The size of this array can break compatibility with some older GPUs
                // This shader is equal to GraphStandard.shader but with a smaller Array size.
                
				uniform float _TotalReservedValues[64];
				uniform float _TotalAllocatedValues[64];
				uniform float _GfxValues[64];
				uniform float _GCHeapValues[64];
				uniform float _GCUsedValues[64];

                uniform float _GraphValues_Length = 64;

                fixed4 frag(v2f IN) : SV_Target
                {
                    fixed4 color = IN.color;

                    fixed xCoord = IN.texcoord.x;
                    fixed yCoord = IN.texcoord.y;

                    float totalReservedValue = _TotalReservedValues[floor(xCoord * _GraphValues_Length)];
					float totalAllocatedValue = _TotalAllocatedValues[floor(xCoord * _GraphValues_Length)];
					float gfxValue = _GfxValues[floor(xCoord * _GraphValues_Length)];
					float gcHeapValue = _GCHeapValues[floor(xCoord * _GraphValues_Length)];
					float gcUsedValue = _GCUsedValues[floor(xCoord * _GraphValues_Length)];
					
                    if (yCoord < gcUsedValue && gcUsedValue > 0)
					{
						color *= _GCUsedColor;
					}
					else if (yCoord < gcHeapValue && gcHeapValue > 0)
					{
						color *= _GCHeapColor;
					}
					else if (yCoord < gfxValue && gfxValue > 0)
					{
						color *= _GfxColor;
					}
					else if (yCoord < totalAllocatedValue && totalAllocatedValue > 0)
					{
						color *= _TotalAllocatedColor;
					}
					else if (yCoord < totalReservedValue && totalReservedValue > 0)
					{
						color *= _TotalReservedColor;
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