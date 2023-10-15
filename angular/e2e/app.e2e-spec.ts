import { ProManTemplatePage } from './app.po';

describe('ProMan App', function() {
  let page: ProManTemplatePage;

  beforeEach(() => {
    page = new ProManTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
