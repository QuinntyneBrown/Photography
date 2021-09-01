export const vw = (v: number) => {
  const h = Math.max(
    document.documentElement.clientWidth,
    window.innerWidth || 0
  );
  return (v * h) / 100;
};
