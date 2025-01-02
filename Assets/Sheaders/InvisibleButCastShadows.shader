Shader"Custom/InvisibleButCastShadows"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

        // Настройки для рендера теней
        Pass
        {
Name"SHADOWCASTER"
            Tags
{"LightMode" = "ShadowCaster"
}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};

struct v2f
{
    float4 pos : SV_POSITION;
};

v2f vert(appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    return fixed4(0, 0, 0, 0); // прозрачный цвет
}
            ENDCG
        }
        
        // Пропускаем рендер в основной проход
        Pass
        {
Name "FORWARD"
            Blend Zero One

ZWrite On

ColorMask 0
        }
    }
}
