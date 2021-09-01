export const vh = (v: number) => {
  const h = Math.max(
    document.documentElement.clientHeight,
    window.innerHeight || 0
  );
  return (v * h) / 100;
};
