export function verificationRequestDto(data) {
  return {
    verificationState: parseInt(data.verificationStatus),
  };
}

export function verificationResponseDto(data) {
  return {
    id: data.id,
    verificationStatus: data.verificationState,
  };
}
