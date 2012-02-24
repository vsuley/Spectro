float4x4 World;
float4x4 View;
float4x4 Projection;

texture DiffuseTexture;
float3 CameraPos;
float3 LightPosition;
float3 LightDiffuseColor; // intensity multiplier
float3 LightSpecularColor; // intensity multiplier
float LightDistanceSquared;
float3 DiffuseColor;
float3 AmbientLightColor;
float3 EmissiveColor;
float3 SpecularColor;
float SpecularPower;

sampler texsampler = sampler_state
{
  Texture = DiffuseTexture;
};

struct VertexShaderInput
{
  float4 Position : POSITION0;
  float3 Normal : NORMAL0;
  float2 TexCoords : TEXCOORD0;
};

struct VertexShaderOutput
{
  float4 Position : POSITION0;
  float2 TexCoords : TEXCOORD0;
  float3 Normal : TEXCOORD1;
  float3 WorldPos : TEXCOORD2;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
  VertexShaderOutput output;

  // "Multiplication will be done in the pre-shader - so no cost per vertex"
  float4x4 viewprojection = mul(View, Projection);
  float4 posWorld = mul(input.Position, World);
  output.Position = mul(posWorld, viewprojection);
  output.TexCoords = input.TexCoords;

  // Passing information on to do both specular AND diffuse calculation in pixel shader
  output.Normal = mul(input.Normal, (float3x3)World);
  output.WorldPos = posWorld;

  return output;
}

float4 PixelShaderFunctionWithoutTex(VertexShaderOutput input) : COLOR0
{
  // Phong relfection is ambient + light-diffuse + spec highlights.
  // I = Ia*ka*Oda + fatt*Ip[kd*Od(N.L) + ks(R.V)^n]
  // Ref: http://www.whisqu.se/per/docs/graphics8.htm
  // and http://en.wikipedia.org/wiki/Phong_shading
  // Get light direction for this fragment
  float3 lightDir = normalize(input.WorldPos - LightPosition);

  // Note: Non-uniform scaling not supported
  float diffuseLighting = saturate(dot(input.Normal, -lightDir)); // per pixel diffuse lighting

  // Introduce fall-off of light intensity
  diffuseLighting *= (LightDistanceSquared / dot(LightPosition - input.WorldPos, LightPosition - input.WorldPos));

  // Using Blinn half angle modification for perofrmance over correctness
  float3 h = normalize(normalize(CameraPos - input.WorldPos) - lightDir);

  float specLighting = pow(saturate(dot(h, input.Normal)), SpecularPower);

  return float4(saturate(
    AmbientLightColor +
    (DiffuseColor * LightDiffuseColor * diffuseLighting * 0.6) + // Use light diffuse vector as intensity multiplier
    (SpecularColor * LightSpecularColor * specLighting * 0.5) // Use light specular vector as intensity multiplier
    ), 1);
}

float4 PixelShaderFunctionWithTex(VertexShaderOutput input) : COLOR0
{
  // Phong relfection is ambient + light-diffuse + spec highlights.
  // I = Ia*ka*Oda + fatt*Ip[kd*Od(N.L) + ks(R.V)^n]
  // Ref: http://www.whisqu.se/per/docs/graphics8.htm
  // and http://en.wikipedia.org/wiki/Phong_shading

  // Get light direction for this fragment
  float3 lightDir = normalize(input.WorldPos - LightPosition); // per pixel diffuse lighting

  // Note: Non-uniform scaling not supported
  float diffuseLighting = saturate(dot(input.Normal, -lightDir));

  // Introduce fall-off of light intensity
  diffuseLighting *= (LightDistanceSquared / dot(LightPosition - input.WorldPos, LightPosition - input.WorldPos));

  // Using Blinn half angle modification for perofrmance over correctness
  float3 h = normalize(normalize(CameraPos - input.WorldPos) - lightDir);
  float specLighting = pow(saturate(dot(h, input.Normal)), SpecularPower);
  float4 texel = tex2D(texsampler, input.TexCoords);

  return float4(saturate(
    AmbientLightColor +
    (texel.xyz * DiffuseColor * LightDiffuseColor * diffuseLighting * 0.6) + // Use light diffuse vector as intensity multiplier
    (SpecularColor * LightSpecularColor * specLighting * 0.5) // Use light specular vector as intensity multiplier
    ), texel.w);
  }

technique TechniqueWithoutTexture
{
  pass Pass1
  {
    VertexShader = compile vs_2_0 VertexShaderFunction();
    PixelShader = compile ps_2_0 PixelShaderFunctionWithoutTex();
  }
}

technique TechniqueWithTexture
{
  pass Pass1
  {
    VertexShader = compile vs_2_0 VertexShaderFunction();
    PixelShader = compile ps_2_0 PixelShaderFunctionWithTex();
  }
}